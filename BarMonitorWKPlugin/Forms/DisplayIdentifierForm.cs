namespace BarMonitorWKPlugIn.Forms {

    public partial class DisplayIdentifierForm : Form {
        public const int DEFAULT_VIEW_TIMEOUT = 3000;

        private readonly int viewTimeout;

        #region Constructor

        public DisplayIdentifierForm(uint displayNumber, string displayName) : this(displayNumber, displayName, DEFAULT_VIEW_TIMEOUT) {
        }

        public DisplayIdentifierForm(uint displayNumber, string displayName, int viewTimeout) {
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
