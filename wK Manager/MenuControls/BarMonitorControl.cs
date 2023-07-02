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

namespace wK_Manager.MenuControls
{
    public partial class BarMonitorControl : WKMenuControl
    {
        private const int IdentifyTimeout = 3000;
        private List<string> VisibleMonitorIdentifiers = new();
        private FolderBrowserDialog fbd = new();
        private OpenFileDialog ofd = new();
        private HttpClient wc = new();

        public BarMonitorControl()
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
                    Name = monitor.MonitorInformation.TargetId.ToString() + "_display",
                    Tag = monitor,
                    Text = monitor.MonitorInformation.SourceId.ToString() + " - " + monitor.MonitorInformation.FriendlyName
                };

                displayButton.CheckedChanged += OnMonitorToggle;
                displayButton.MouseDown += OnMonitorRightClick;
                displaysFlowLayoutPanel.Controls.Add(displayButton);
            }

            fbd.ShowNewFolderButton = true;
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "VLC Player|vlc.exe";
            ofd.Multiselect = false;
            ofd.DefaultExt = "exe";
            ofd.OkRequiresInteraction = true;
            ofd.ValidateNames = true;
            ofd.Title = "VLC Pfad auswählen ...";
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
            }
        }

        private void localPathButton_Click(object sender, EventArgs e)
        {
            fbd.InitialDirectory = localPathTextBox.Text != null && localPathTextBox.Text.Trim() != string.Empty
                ? localPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (fbd.ShowDialog() == DialogResult.OK && Directory.Exists(fbd.SelectedPath))
            {
                localPathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void vlcPathButton_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = vlcPathTextBox.Text != null && vlcPathTextBox.Text.Trim() != string.Empty
                ? vlcPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (ofd.ShowDialog() == DialogResult.OK && File.Exists(ofd.FileName))
            {
                vlcPathTextBox.Text = ofd.FileName;
            }
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
                    Uri? uri = Uri.IsWellFormedUriString(tb.Text, UriKind.Absolute) ? new(tb.Text) : null;
                    HttpResponseMessage? response = uri != null ? await wc.GetAsync(uri) : null;

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        remotePathStatusLabel.ForeColor = Color.DarkGreen;
                        remotePathStatusLabel.Text = "OK";

                        //TODO
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
    }
}
