namespace wK_Manager.Forms {

    public partial class DisplayIdentifierForm : Form {
        private readonly int viewTimeout = 3000;

        #region Constructor

        public DisplayIdentifierForm(uint displayNumber, string displayName, int viewTimeout = 3000) {
            this.viewTimeout = viewTimeout;
            InitializeComponent();

            displayInfoLabel.Text = displayInfoLabel.Text.Replace("%DisplayNumber%", displayNumber.ToString())
                                                         .Replace(
                                                            "%DisplayName%",
                                                            displayName.Replace(" & ", " &" + Environment.NewLine)
                                                         );
        }

        #endregion Constructor

        #region EventHandlers

        private void displayIdentifierForm_Shown(object sender, EventArgs e) {
            System.Windows.Forms.Timer timer = new() { Enabled = true, Interval = viewTimeout };
            timer.Tick += onTimerTick;
        }

        private void onTimerTick(object? sender, EventArgs e)
            => Close();

        #endregion EventHandlers
    }
}
