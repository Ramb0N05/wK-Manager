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

namespace wK_Manager.MenuControls
{
    public partial class BarMonitorControl : WKMenuControl
    {
        private const int IdentifyTimeout = 3000;
        private List<string> VisibleMonitorIdentifiers = new();

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
    }
}
