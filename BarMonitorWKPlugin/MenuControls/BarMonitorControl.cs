using wK_Manager.Base;
using wK_Manager.Forms;
using System.Net.Http.Headers;
using SharpRambo.ExtensionsLib;
using BarMonitorWKPlugIn.Forms;
using DotNet.Basics.SevenZip;
using BarMonitorWKPlugIn.Base;
using WindowsDisplayAPI;
using wK_Manager.Base.Extensions;
using wK_Manager.Base.Providers;

namespace wK_Manager.PlugIns.MenuControls {

    public partial class BarMonitorControl : WKMenuControl {
        private const string DisplayButtonPrefix = "display_";
        private const int IdentifyTimeout = 3000;

        private readonly FolderBrowserDialog fbd = new();
        private readonly OpenFileDialog ofd = new();
        private readonly BarMonitorWKPlugIn plugIn;
        private BarMonitorControlConfig config = new();
        private PresenterForm? presenter;

        public override IWKMenuControlConfig Config { get => config; set => config = value as BarMonitorControlConfig ?? new(); }
        public IEnumerable<string> VisibleMonitorIdentifiers { get; private set; } = Enumerable.Empty<string>();

        #region Constructor

        public BarMonitorControl(BarMonitorWKPlugIn sender) : base(sender.Base) {
            plugIn = sender;
            MenuImageKey = plugIn.ImageKey;

            InitializeComponent();

            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.OkRequiresInteraction = true;
            ofd.RestoreDirectory = false;
            ofd.ValidateNames = true;

            intervalNumericUpDown.Maximum = decimal.MaxValue;
            reloadMonitorsPictureBox.Image = Base.GetImage("sign_sync", reloadMonitorsPictureBox.Size.GetWithAspectRatio());
        }

        #endregion Constructor

        #region Overrides

        public override IWKMenuControlConfig? ConfigFromControls() {
            DisplayTarget? targetInfo = getSelectedDisplay();

            config.DisplayTarget = targetInfo?.Identifier;
            config.LocalDiashowPath = localPathTextBox.Text;
            config.RemoteDiashowPath = remotePathTextBox.Text;
            config.AutoObtainDiashow = autoObtainCheckBox.Checked;
            config.Interval = (uint)intervalNumericUpDown.Value;
            config.Repeat = repeatCheckBox.Checked;
            config.Shuffle = shuffleCheckBox.Checked;
            config.LockedConfig = lockSettingsCheckBox.Checked;

            return config;
        }

        public override void ConfigToControls(IWKMenuControlConfig config) {
            if (config is BarMonitorControlConfig conf) {
                _ = selectMonitorFromConfig(conf.DisplayTarget);

                localPathTextBox.Text = conf.LocalDiashowPath;
                remotePathTextBox.Text = conf.RemoteDiashowPath;
                autoObtainCheckBox.Checked = conf.AutoObtainDiashow;
                intervalNumericUpDown.Value = conf.Interval > intervalNumericUpDown.Minimum ? conf.Interval : intervalNumericUpDown.Minimum;
                repeatCheckBox.Checked = conf.Repeat;
                shuffleCheckBox.Checked = conf.Shuffle;

                lockSettingsCheckBox.Checked = conf.LockedConfig;
            }
        }

        #endregion Overrides

        #region Methods

        private static string getDisplayButtonName(string? displayName)
                    => DisplayButtonPrefix + displayName;

        private static async Task<(bool Status, string? ContentType, Exception? Error)?> validateRemotePath(string remotePath) {
            (bool Status, string? ContentType, Exception? Error)? result = new() {
                Status = false,
                ContentType = string.Empty,
                Error = null
            };

            Uri? uri = Uri.IsWellFormedUriString(remotePath, UriKind.Absolute) ? new(remotePath) : null;
            if (uri != null) {
                using HttpRequestMessage request = new(HttpMethod.Head, uri);

                try {
                    using HttpResponseMessage response = await NetworkingProvider.HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    MediaTypeHeaderValue? contentType = response.Content.Headers.ContentType;

                    result = new() {
                        Status = contentType != null && BarMonitorWKPlugIn.AcceptedDiashowArchiveContentTypes.Contains(contentType.MediaType),
                        ContentType = contentType?.MediaType,
                        Error = null
                    };
                } catch (Exception ex) {
                    result = new() {
                        Status = false,
                        ContentType = string.Empty,
                        Error = ex
                    };
                }
            }

            return result;
        }

        private async Task createDisplayTargets() {
            displaysFlowLayoutPanel.Controls.Clear();
            IEnumerable<DisplayTarget> targets = await DisplayTarget.Initialize(Display.GetDisplays());

            await targets.ForEachAsync(async (target) => {
                FontStyle fontStyle = target.IsPrimary ? FontStyle.Bold : FontStyle.Regular;

                CheckBox displayButton = new() {
                    Appearance = Appearance.Button,
                    AutoSize = true,
                    Name = getDisplayButtonName(target.Identifier),
                    Tag = target,
                    Text = target.Number + Environment.NewLine + "(" + target.FriendlyName.Replace("&", "&&") + ")",
                    TextAlign = ContentAlignment.MiddleCenter
                };

                int itemHeight = displaysFlowLayoutPanel.Height
                    - displaysFlowLayoutPanel.Padding.Vertical
                    - displayButton.Padding.Vertical
                    - displayButton.Margin.Vertical;

                displayButton.MinimumSize = new Size(itemHeight, itemHeight);
                displayButton.Font = new Font(displayButton.Font, fontStyle);
                displayButton.CheckedChanged += onMonitorToggle;
                displayButton.MouseDown += onMonitorRightClick;
                displaysFlowLayoutPanel.Controls.Add(displayButton);

                await Task.CompletedTask;
            });
        }

        private async Task<bool> downloadFromRemotePath(string remotePath, DirectoryInfo downloadDirectory) {
            (bool Status, string? ContentType, Exception? Error)? check = await validateRemotePath(remotePath);

            if (check.HasValue) {
                string[] exts = MimeMapping.MimeUtility.GetExtensions(check.Value.ContentType);

                if (check.Value.Status) {
                    Uri uri = new(remotePath);
                    string downloadPath = Path.Combine(downloadDirectory.FullName, "diashow." + exts?.FirstOrDefault() ?? "zip");

                    try {
                        await downloadDirectory.DeleteContents();
                        Progress<float> downloadProgress = new((p) => obtainProgressBar.Value = (int)(p * 100));
                        DirectoryInfo extractDirectory = new(Path.Combine(downloadDirectory.FullName, "tmp"));
                        FileStream fStream = new(downloadPath, FileMode.Create);

                        obtainProgressBar.Value = 0;
                        obtainProgressBar.Visible = true;

                        await NetworkingProvider.HttpClient.DownloadAsync(uri.AbsoluteUri, fStream, downloadProgress);
                        await fStream.FlushAsync();
                        fStream.Close();

                        obtainProgressBar.Visible = false;
                        SevenZipExe sevenZipExe = new();

                        if (await sevenZipExe.ExtractAtLocalPath(downloadPath, extractDirectory)) {
                            await extractDirectory.CopyTo(downloadDirectory.FullName);
                            extractDirectory.Delete(true);
                            await downloadDirectory.DeleteDirectories();

                            return true;
                        }

                        return false;
                    } catch (Exception) {
                        return false;
                    }
                } else
                    return false;
            } else
                return false;
        }

        private DisplayTarget? getSelectedDisplay() {
            foreach (Control c in displaysFlowLayoutPanel.Controls)
                if (c is CheckBox cb && c.Tag != null && c.Tag is DisplayTarget target && cb.Checked)
                    return target;

            return null;
        }

        private async Task selectMonitorFromConfig(string? displayTarget) {
            if (displayTarget.IsNull())
                return;

            CheckBox? detectedMonitorCb = await Task.Run(() => displaysFlowLayoutPanel.Controls
                .Cast<Control>()
                .FirstOrDefault((c) => c?.Name == getDisplayButtonName(displayTarget) && c is CheckBox, null)
                as CheckBox
            );

            detectedMonitorCb ??= await Task.Run(() => displaysFlowLayoutPanel.Controls
                .Cast<Control>()
                .FirstOrDefault((c) => c is CheckBox cb && cb.Tag is DisplayTarget target && target.Identifier == displayTarget, null)
                as CheckBox
            );

            if (detectedMonitorCb != null)
                detectedMonitorCb.Checked = true;
        }

        #endregion Methods

        #region EventHandlers

        private async void barMonitorControl_Load(object sender, EventArgs e) {
            await createDisplayTargets();
            await LoadConfig();
        }

        private void barMonitorControl_Resize(object sender, EventArgs e)
            => reloadMonitorsPictureBox.Location = new Point(
                reloadMonitorsPictureBox.Margin.Left,
                displaysGroupBox.Height - (int)(reloadMonitorsPictureBox.Height / 2.5)
            );

        private void defaultsButton_Click(object sender, EventArgs e)
            => ConfigToControls(Config);

        private void intervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (presenter != null && sender is NumericUpDown nud)
                presenter.Interval = (uint)nud.Value;
        }

        private void localPathButton_Click(object sender, EventArgs e) {
            fbd.Reset();
            fbd.ShowNewFolderButton = true;
            fbd.InitialDirectory = localPathTextBox.Text != null && localPathTextBox.Text.Trim() != string.Empty
                ? localPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fbd.ShowDialog() == DialogResult.OK && Directory.Exists(fbd.SelectedPath))
                localPathTextBox.Text = fbd.SelectedPath;
        }

        private void lockSettingsCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (sender is CheckBox cb) {
                displaysGroupBox.Enabled = !cb.Checked;
                diashowGroupBox.Enabled = !cb.Checked;
                identifyLabel.Enabled = !cb.Checked;
            }
        }

        private async void obtainButton_Click(object sender, EventArgs e) {
            if (!remotePathTextBox.Text.IsNull() && sender is Button b) {
                b.Enabled = false;

                b.ForeColor = await downloadFromRemotePath(remotePathTextBox.Text, new DirectoryInfo(localPathTextBox.Text))
                    ? Color.DarkGreen
                    : Color.DarkRed;

                b.Enabled = true;
            }
        }

        private void onMonitorRightClick(object? sender, MouseEventArgs e) {
            if (sender is CheckBox cb && e.Button == MouseButtons.Right) {
                DisplayTarget target = cb.Tag as DisplayTarget ?? DisplayTarget.Unknown;

                if (!VisibleMonitorIdentifiers.Any(id => id == target.Identifier)) {
                    Point monitorLocation = target.GetPosition(new Point(15, 15));
                    Form idForm = new DisplayIdentifierForm(target.Number, target.FriendlyName) {
                        Name = target.Identifier,
                        StartPosition = FormStartPosition.Manual,
                        Location = monitorLocation
                    };

                    VisibleMonitorIdentifiers = VisibleMonitorIdentifiers.Append(idForm.Name);
                    idForm.Show();

                    System.Windows.Forms.Timer timer = new() { Enabled = true, Interval = IdentifyTimeout, Tag = idForm };
                    timer.Tick += onTimerTick;
                }
            }
        }

        private void onMonitorToggle(object? sender, EventArgs e) {
            if (sender is CheckBox cb && cb.Checked) {
                foreach (CheckBox cbCtl in displaysFlowLayoutPanel.Controls.OfType<CheckBox>())
                    if (cbCtl.Name != cb.Name)
                        cbCtl.Checked = false;
            }
        }

        private void onTimerTick(object? sender, EventArgs e) {
            if (sender is System.Windows.Forms.Timer t && t.Tag is Form f) {
                VisibleMonitorIdentifiers = VisibleMonitorIdentifiers.SelectMany(
                    id => id != f.Name ? new[] { id } : Enumerable.Empty<string>()
                );

                f.Close();
            }
        }

        private async void presentButton_Click(object sender, EventArgs e) {
            string diashowPath = localPathTextBox.Text;

            if (plugIn != null) {
                if (presenter?.Visible == true)
                    presenter.Close();

                if (autoObtainCheckBox.Checked && !remotePathTextBox.Text.IsNull()) {
                    obtainButton.Enabled = false;

                    obtainButton.ForeColor = await downloadFromRemotePath(remotePathTextBox.Text, new DirectoryInfo(localPathTextBox.Text))
                        ? Color.DarkGreen
                        : Color.DarkRed;

                    obtainButton.Enabled = true;
                }

                DisplayTarget? selectedTarget = getSelectedDisplay();

                presenter = new PresenterForm(diashowPath) {
                    Interval = (uint)intervalNumericUpDown.Value,
                    Location = selectedTarget?.GetPosition() ?? new Point(0, 0),
                    Repeat = repeatCheckBox.Checked,
                    Shuffle = shuffleCheckBox.Checked,
                    StartPosition = FormStartPosition.Manual,
                    TopLevel = true,
                    TopMost = true,
                    WindowState = FormWindowState.Maximized
                };

                presenter.Show();
            }
        }

        private async void reloadMonitorsPictureBox_Click(object sender, EventArgs e) {
            await createDisplayTargets();
            await selectMonitorFromConfig(config.DisplayTarget);
        }

        private async void remotePathTextBox_TextChanged(object sender, EventArgs e) {
            if (sender is TextBox tb) {
                if (!tb.Text.Trim().IsNull()) {
                    (bool Status, string? ContentType, Exception? Error)? check = await validateRemotePath(tb.Text);

                    if (check.HasValue) {
                        if (check.Value.Status) {
                            remotePathStatusLabel.ForeColor = Color.DarkGreen;
                            remotePathStatusLabel.Text = "OK";
                        } else {
                            remotePathStatusLabel.ForeColor = Color.DarkRed;
                            remotePathStatusLabel.Text = "Fehler";
                        }
                    }
                }

                remotePathStatusLabel.Text = string.Empty;
            }
        }

        private void repeatCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (presenter != null && sender is CheckBox cb)
                presenter.Repeat = cb.Checked;
        }

        private async void saveButton_Click(object sender, EventArgs e)
            => await SaveConfig();

        private void shuffleCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (presenter != null && sender is CheckBox cb)
                presenter.Shuffle = cb.Checked;
        }

        private void stopButton_Click(object sender, EventArgs e) {
            if (presenter?.Visible == true)
                presenter.Close();
        }

        #endregion EventHandlers
    }
}
