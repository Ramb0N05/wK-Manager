namespace wK_Manager.Forms {
    public partial class DisplayIdentifierForm : Form {
        public int ViewTimeout = 3000;

        public DisplayIdentifierForm(uint displayNumber, string displayName, int viewTimeout = 3000) {
            ViewTimeout = viewTimeout;
            InitializeComponent();

            displayInfoLabel.Text = displayInfoLabel.Text.Replace("%DisplayNumber%", displayNumber.ToString())
                                                         .Replace(
                                                            "%DisplayName%",
                                                            displayName.Replace(" & ", " &" + Environment.NewLine)
                                                         );
        }

        private void displayIdentifierForm_Shown(object sender, EventArgs e) {
            System.Windows.Forms.Timer timer = new() { Enabled = true, Interval = ViewTimeout };
            timer.Tick += onTimerTick;
        }

        private void onTimerTick(object? sender, EventArgs e)
            => Close();
    }
}
