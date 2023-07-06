using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wK_Manager.Forms
{
    public partial class DisplayIdentifierForm : Form
    {
        public int ViewTimeout = 3000;

        public DisplayIdentifierForm(uint displayIdentifier, string displayName, int viewTimeout = 3000)
        {
            ViewTimeout = viewTimeout;
            InitializeComponent();

            displayInfoLabel.Text = displayInfoLabel.Text.Replace("%DisplayIdentifier%", displayIdentifier.ToString())
                                                         .Replace("%DisplayName%", displayName);
        }

        private void DisplayIdentifierForm_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new() { Enabled = true, Interval = ViewTimeout };
            timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
