using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;
using ScreenInformation;
using wK_Manager.Forms;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using SharpRambo.ExtensionsLib;

namespace wK_Manager.MenuControls
{
    public partial class BarMonitorControl : WKMenuControl
    {
        private const int IdentifyTimeout = 3000;
        private const string displayButtonPostfix = "_display";

        private List<string> VisibleMonitorIdentifiers = new();
        private FolderBrowserDialog fbd = new();
        private OpenFileDialog ofd = new();
        private HttpClient httpCli = new();

        private BarMonitorControlConfig config = new();
        public override IWKMenuControlConfig Config { get => config; set => config = value as BarMonitorControlConfig ?? new(); }
        public readonly string ConfigFilePath = MainForm.Config.GetUserConfigFilePath(BarMonitorControlConfig.ConfigFileName);

        public BarMonitorControl() : base()
        {
            InitializeComponent();

            foreach (DisplaySource monitor in ScreenManager.GetDetailedMonitors())
            {
                CheckBox initCb = new();
                int itemHeight = displaysFlowLayoutPanel.Height - displaysFlowLayoutPanel.Padding.Vertical -
                                 initCb.Padding.Vertical - initCb.Margin.Vertical;

                CheckBox displayButton = new()
                {
                    Appearance = Appearance.Button,
                    AutoSize = true,
                    MinimumSize = new Size(5, itemHeight),
                    Name = monitor.MonitorInformation.TargetId.ToString() + displayButtonPostfix,
                    Tag = monitor,
                    Text = monitor.MonitorInformation.SourceId.ToString() + " - " + monitor.MonitorInformation.FriendlyName
                };

                /*if (monitor.MonitorInformation.TargetId == config.MonitorTargetID)
                    displayButton.Checked = true;*/

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
                autoGetCheckBox.Checked = conf.AutoObtainDiashow;
                vlcPathTextBox.Text = conf.VLCPath;
                lockSettingsCheckBox.Checked = conf.LockedConifg;
            }
        }

        public override IWKMenuControlConfig? ConfigFromControls()
        {
            foreach (Control c in displaysFlowLayoutPanel.Controls)
            {
                if (c is CheckBox cb && c.Tag != null && c.Tag is DisplaySource monitor && cb.Checked)
                {
                    config.MonitorSourceID = monitor.MonitorInformation.SourceId;
                    config.MonitorTargetID = monitor.MonitorInformation.TargetId;
                    break;
                }
            }

            config.LocalDiashowPath = localPathTextBox.Text;
            config.RemoteDiashowPath = remotePathTextBox.Text;
            config.AutoObtainDiashow = autoGetCheckBox.Checked;
            config.VLCPath = vlcPathTextBox.Text;
            config.LockedConifg = lockSettingsCheckBox.Checked;

            return config;
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
                    Form idForm = new DisplayIdentifierForm(displayId, displayName)
                    {
                        Name = displayName.Trim(),
                        StartPosition = FormStartPosition.Manual,
                        Left = Screen.AllScreens[displayId].WorkingArea.Location.X + 15,
                        Top = Screen.AllScreens[displayId].WorkingArea.Location.Y + 15
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
                vlcPathTextBox.Enabled = !cb.Checked;
                vlcPathButton.Enabled = !cb.Checked;
                vlcPathLabel.Enabled = !cb.Checked;
                saveButton.Enabled = !cb.Checked;
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

        private void vlcPathButton_Click(object sender, EventArgs e)
        {
            ofd.DefaultExt = "exe";
            ofd.Filter = "VLC Player|vlc.exe";
            ofd.Title = "VLC Pfad auswählen ...";

            ofd.InitialDirectory = vlcPathTextBox.Text != null && vlcPathTextBox.Text.Trim() != string.Empty
                ? vlcPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (ofd.ShowDialog() == DialogResult.OK && File.Exists(ofd.FileName))
                vlcPathTextBox.Text = ofd.FileName;
        }

        private void presentButton_Click(object sender, EventArgs e)
        {

        }

        private void stopButton_Click(object sender, EventArgs e)
        {

        }

        private async void remotePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is not null and TextBox tb)
            {
                if (tb.Text.Trim() != string.Empty)
                {
                    if (await validateRemotePath(tb.Text))
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
                else
                    remotePathStatusLabel.Text = string.Empty;
            }
        }

        private async Task<bool> validateRemotePath(string remotePath)
        {
            Uri? uri = Uri.IsWellFormedUriString(remotePath, UriKind.Absolute) ? new(remotePath) : null;
            HttpResponseMessage? response = null;

            try
            {
                response = uri != null ? await httpCli.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead) : null;
            }
            catch (Exception) { }

            if (response == null || !response.IsSuccessStatusCode)
                return false;

            MediaTypeHeaderValue? contentType = response.Content.Headers.ContentType;
            return contentType != null && Properties.Settings.Default.acceptedDiashowContentTypes.Contains(contentType.MediaType);
        }

        private async void BarMonitorControl_Load(object sender, EventArgs e)
        {
            await LoadConfig(ConfigFilePath);
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            await SaveConfig(ConfigFilePath);
        }
    }
}
