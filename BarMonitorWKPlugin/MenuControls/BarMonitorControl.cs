using wK_Manager.Base;
using ScreenInformation;
using wK_Manager.Forms;
using System.Net.Http.Headers;
using SharpRambo.ExtensionsLib;
using BarMonitorWKPlugin.Forms;
using DotNet.Basics.SevenZip;

namespace wK_Manager.Plugins.MenuControls
{
    public partial class BarMonitorControl : WKMenuControl
    {
        private const int IdentifyTimeout = 3000;
        private const string displayButtonPostfix = "_display";

        private BarMonitorWKPlugin? sender = null;
        private PresenterForm? presenter = null;
        private List<string> VisibleMonitorIdentifiers = new();
        private FolderBrowserDialog fbd = new();
        private OpenFileDialog ofd = new();

        private BarMonitorControlConfig config = new();
        public override IWKMenuControlConfig Config { get => config; set => config = value as BarMonitorControlConfig ?? new(); }

        public BarMonitorControl(object sender) : base(sender)
        {
            if (sender is BarMonitorWKPlugin plugin)
                this.sender = plugin;

            InitializeComponent();

            foreach (DisplaySource monitor in ScreenManager.GetDetailedMonitors())
            {
                FontStyle fontStyle = monitor.MonitorInformation.IsPrimary ? FontStyle.Bold : FontStyle.Regular;
                string friendlyName = !monitor.MonitorInformation.FriendlyName.IsNull()
                                    ? monitor.MonitorInformation.FriendlyName
                                    : "{" + monitor.MonitorInformation.TargetId.ToString() + "}";
                
                CheckBox displayButton = new()
                {
                    Appearance = Appearance.Button,
                    AutoSize = true,
                    Name = monitor.MonitorInformation.TargetId.ToString() + displayButtonPostfix,
                    Tag = monitor,
                    Text = monitor.MonitorInformation.SourceId.ToString() + Environment.NewLine + friendlyName,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                int itemHeight = displaysFlowLayoutPanel.Height - displaysFlowLayoutPanel.Padding.Vertical -
                                 displayButton.Padding.Vertical - displayButton.Margin.Vertical;

                displayButton.MinimumSize = new Size(itemHeight, itemHeight);
                displayButton.Font = new Font(displayButton.Font, fontStyle);
                displayButton.CheckedChanged += OnMonitorToggle;
                displayButton.MouseDown += OnMonitorRightClick;
                displaysFlowLayoutPanel.Controls.Add(displayButton);
            }

            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.OkRequiresInteraction = true;
            ofd.RestoreDirectory = false;
            ofd.ValidateNames = true;

            intervalNumericUpDown.Maximum = decimal.MaxValue;
        }

        public override void ConfigToControls(IWKMenuControlConfig config)
        {
            if (config is BarMonitorControlConfig conf)
            {
                bool detectedMonitor = false;
                foreach (Control c in displaysFlowLayoutPanel.Controls)
                {
                    if (c.Name == conf.MonitorTargetID + displayButtonPostfix && c is CheckBox cb)
                    {
                        cb.Checked = true;
                        detectedMonitor = true;
                        break;
                    }
                }

                if (!detectedMonitor)
                {
                    foreach (Control c in displaysFlowLayoutPanel.Controls)
                    {
                        if (c is CheckBox cb && c.Tag != null && c.Tag is DisplaySource monitor && monitor.MonitorInformation.SourceId == conf.MonitorSourceID)
                        {
                            cb.Checked = true;
                            detectedMonitor = true;
                            break;
                        }
                    }
                }

                localPathTextBox.Text = conf.LocalDiashowPath;
                remotePathTextBox.Text = conf.RemoteDiashowPath;
                autoObtainCheckBox.Checked = conf.AutoObtainDiashow;
                intervalNumericUpDown.Value = conf.Interval > intervalNumericUpDown.Minimum ? conf.Interval : intervalNumericUpDown.Minimum;
                repeatCheckBox.Checked = conf.Repeat;
                shuffleCheckBox.Checked = conf.Shuffle;

                lockSettingsCheckBox.Checked = conf.LockedConfig;
            }
        }

        public override IWKMenuControlConfig? ConfigFromControls()
        {
            MonitorInformation? monitorInfo = GetSelectedMonitor();

            config.MonitorSourceID = monitorInfo?.SourceId ?? 0;
            config.MonitorTargetID = monitorInfo?.TargetId ?? 0;
            config.LocalDiashowPath = localPathTextBox.Text;
            config.RemoteDiashowPath = remotePathTextBox.Text;
            config.AutoObtainDiashow = autoObtainCheckBox.Checked;
            config.Interval = (uint)intervalNumericUpDown.Value;
            config.Repeat = repeatCheckBox.Checked;
            config.Shuffle = shuffleCheckBox.Checked;
            config.LockedConfig = lockSettingsCheckBox.Checked;

            return config;
        }

        private MonitorInformation? GetSelectedMonitor()
        {
            MonitorInformation? monitorInfo = null;

            foreach (Control c in displaysFlowLayoutPanel.Controls)
                if (c is CheckBox cb && c.Tag != null && c.Tag is DisplaySource monitor && cb.Checked)
                    monitorInfo = monitor.MonitorInformation;

            return monitorInfo;
        }

        private Point GetLocationAtMonitor(uint monitorId, Point? location = null)
        {
            if (location == null)
                location = new Point(0, 0);

            if (Screen.AllScreens.Length > monitorId && location != null)
            {
                Screen screen = Screen.AllScreens[monitorId];

                if (location.Value.X <= screen.WorkingArea.Width && location.Value.X >= 0 &&
                    location.Value.Y <= screen.WorkingArea.Height && location.Value.Y >= 0)
                    return new Point(Screen.AllScreens[monitorId].WorkingArea.Location.X + location.Value.X,
                             Screen.AllScreens[monitorId].WorkingArea.Location.Y + location.Value.Y);
            }

            return new Point(0, 0);
        }

        private void OnMonitorToggle(object? sender, EventArgs e)
        {
            if (sender is not null and CheckBox cb && cb.Checked)
            {
                foreach (CheckBox cbCtl in displaysFlowLayoutPanel.Controls.OfType<CheckBox>())
                    if (cbCtl.Name != cb.Name)
                        cbCtl.Checked = false;
            }
        }

        private void OnMonitorRightClick(object? sender, MouseEventArgs e)
        {
            if (sender is not null and CheckBox cb && e.Button == MouseButtons.Right)
            {
                DisplaySource? monitor = cb.Tag as DisplaySource;
                uint displayId = monitor != null ? (uint)monitor.MonitorInformation.SourceId : 0;
                string displayName = monitor != null ? (monitor.MonitorInformation.FriendlyName ?? "Unknown") : "Unknown";

                if (!VisibleMonitorIdentifiers.Contains(displayName.Trim()))
                {
                    Point monitorLocation = GetLocationAtMonitor(displayId, new Point(15, 15));
                    Form idForm = new DisplayIdentifierForm(displayId, displayName)
                    {
                        Name = displayName.Trim(),
                        StartPosition = FormStartPosition.Manual,
                        Location = monitorLocation
                    };

                    VisibleMonitorIdentifiers.Add(idForm.Name);
                    idForm.Show();

                    System.Windows.Forms.Timer timer = new() { Enabled = true, Interval = IdentifyTimeout, Tag = idForm };
                    timer.Tick += OnTimerTick;
                }
            }
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (sender is not null and System.Windows.Forms.Timer t && t.Tag is Form f)
            {
                f.Close();
                VisibleMonitorIdentifiers.Remove(f.Name);
            }
        }

        private void lockSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not null and CheckBox cb)
            {
                displaysGroupBox.Enabled = !cb.Checked;
                diashowGroupBox.Enabled = !cb.Checked;
                identifyLabel.Enabled = !cb.Checked;
            }
        }

        private void localPathButton_Click(object sender, EventArgs e)
        {
            fbd.Reset();
            fbd.ShowNewFolderButton = true;
            fbd.InitialDirectory = localPathTextBox.Text != null && localPathTextBox.Text.Trim() != string.Empty
                ? localPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fbd.ShowDialog() == DialogResult.OK && Directory.Exists(fbd.SelectedPath))
                localPathTextBox.Text = fbd.SelectedPath;
        }

        private async void presentButton_Click(object sender, EventArgs e)
        {
            string diashowPath = localPathTextBox.Text;

            if (this.sender != null)
            {
                if (presenter != null && presenter.Visible)
                    presenter.Close();

                if (autoObtainCheckBox.Checked && !remotePathTextBox.Text.IsNull())
                {
                    obtainButton.Enabled = false;

                    obtainButton.ForeColor = await downloadFromRemotePath(remotePathTextBox.Text, new DirectoryInfo(localPathTextBox.Text))
                        ? Color.DarkGreen
                        : Color.DarkRed;

                    obtainButton.Enabled = true;
                }

                presenter = new PresenterForm(diashowPath)
                {
                    Interval = (uint)intervalNumericUpDown.Value,
                    Location = GetLocationAtMonitor((uint)(GetSelectedMonitor()?.SourceId ?? 0)),
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

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (presenter != null && presenter.Visible)
                presenter.Close();
        }

        private async void remotePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is not null and TextBox tb)
            {
                if (tb.Text.Trim() != string.Empty)
                {
                    dynamic? check = await validateRemotePath(tb.Text);

                    if (check != null)
                    {
                        if (check.Status)
                        {
                            remotePathStatusLabel.ForeColor = Color.DarkGreen;
                            remotePathStatusLabel.Text = "OK";
                        }
                        else
                        {
                            remotePathStatusLabel.ForeColor = Color.DarkRed;
                            remotePathStatusLabel.Text = "Fehler";
                        }
                    }
                }
                else
                    remotePathStatusLabel.Text = string.Empty;
            }
        }

        private async Task<object?> validateRemotePath(string remotePath)
        {
            dynamic? result = new {
                Status = false,
                ContentType = string.Empty,
                Error = false
            };

            if (sender != null)
            {
                Uri? uri = Uri.IsWellFormedUriString(remotePath, UriKind.Absolute) ? new(remotePath) : null;
                if (uri != null)
                {
                    using HttpRequestMessage request = new(HttpMethod.Head, uri);

                    try
                    {
                        using HttpResponseMessage response = await sender.HttpCli.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        MediaTypeHeaderValue? contentType = response.Content.Headers.ContentType;

                        result = new
                        {
                            Status = contentType != null && BarMonitorWKPlugin.AcceptedDiashowArchiveContentTypes.Contains(contentType.MediaType),
                            ContentType = contentType?.MediaType,
                            Error = false
                        };
                    }
                    catch (Exception ex) {
                        result = new {
                            Status = false,
                            ContentType = string.Empty,
                            Error = ex
                        };
                    }
                }
            } else
                result = null;

            return result;
        }

        private async Task<bool> downloadFromRemotePath(string remotePath, DirectoryInfo downloadDirectory)
        {
            if (sender != null)
            {
                dynamic? check = await validateRemotePath(remotePath);

                if (check != null)
                {
                    string[] exts = MimeMapping.MimeUtility.GetExtensions(check.ContentType);

                    if (check.Status)
                    {
                        Uri uri = new(remotePath);
                        string downloadPath = Path.Combine(downloadDirectory.FullName, "diashow." + exts?.FirstOrDefault() ?? "zip");

                        try
                        {
                            await downloadDirectory.DeleteContents();
                            Progress<float> downloadProgress = new((p) => obtainProgressBar.Value = (int)(p * 100));
                            DirectoryInfo extractDirectory = new(Path.Combine(downloadDirectory.FullName, "tmp"));
                            FileStream fStream = new(downloadPath, FileMode.Create);

                            obtainProgressBar.Value = 0;
                            obtainProgressBar.Visible = true;

                            await sender.HttpCli.DownloadAsync(uri.AbsoluteUri, fStream, downloadProgress);
                            await fStream.FlushAsync();
                            fStream.Close();

                            obtainProgressBar.Visible = false;

                            if (await extractAtLocalPath(downloadPath, extractDirectory))
                            {
                                await extractDirectory.CopyTo(downloadDirectory.FullName);
                                extractDirectory.Delete(true);
                                await downloadDirectory.DeleteDirectories();

                                return true;
                            }
                            else
                                return false;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            } else
                return false;
        }

        private async Task<bool> extractAtLocalPath(string filePath, DirectoryInfo extractDirectory)
        {
            if (extractDirectory.Exists)
                extractDirectory.Delete(true);

            if (!filePath.IsNull() && File.Exists(filePath))
            {
                int result = 1;

                await Task.Run(() =>
                {
                    SevenZipExe sevenZipExe = new();
                    result = sevenZipExe.ExtractToDirectory(filePath, extractDirectory.FullName);
                });

                if (result == 0)
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private async void BarMonitorControl_Load(object sender, EventArgs e)
        {
            await LoadConfig();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            await SaveConfig();
        }

        private void defaultsButton_Click(object sender, EventArgs e)
        {
            ConfigToControls(Config);
        }

        private async void obtainButton_Click(object sender, EventArgs e)
        {
            if (!remotePathTextBox.Text.IsNull() && sender is Button b)
            {
                b.Enabled = false;

                b.ForeColor = await downloadFromRemotePath(remotePathTextBox.Text, new DirectoryInfo(localPathTextBox.Text))
                    ? Color.DarkGreen
                    : Color.DarkRed;

                b.Enabled = true;
            }
        }

        private void autoObtainCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void shuffleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (presenter != null && sender is CheckBox cb)
                presenter.Shuffle = cb.Checked;
        }

        private void repeatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (presenter != null && sender is CheckBox cb)
                presenter.Repeat = cb.Checked;
        }

        private void intervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (presenter != null && sender is NumericUpDown nud)
                presenter.Interval = (uint)nud.Value;
        }
    }
}
