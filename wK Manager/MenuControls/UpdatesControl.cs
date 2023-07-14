using System.Reflection;
using wK_Manager.Base;
using wK_Manager.Base.Extensions;

namespace wK_Manager.MenuControls {

    public partial class UpdatesControl : WKMenuControl {
        public static readonly string UpdateDownloadFilePath = Path.Combine(Application.StartupPath, "latest-update.7z");

        private readonly HttpClient httpClient = new(new HttpClientHandler() { UseProxy = false });
        private readonly MainForm main;
        private readonly string updateManifestURL = Properties.Settings.Default.updateManifestURL;

        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #region Constructor

        public UpdatesControl(object sender) : base(sender) {
            InitializeComponent();
            main = sender as MainForm ?? throw new ArgumentNullException(nameof(sender));
        }

        #endregion Constructor

        #region Methods

        public new void Dispose() {
            httpClient.Dispose();
            base.Dispose();
        }

        private async Task checkUpdates() {
            updateButton.Visible = false;
            updateStatusLabel.ForeColor = SystemColors.ControlText;
            updateStatusLabel.Text = "Prüfe auf updates ...";
            updateProgressBar.Visible = false;
            updateProgressLabel.Visible = false;
            VersionData? versionData = await VersionData.GetCurrent(updateManifestURL, httpClient);

            if (versionData != null) {
                Version? assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

                if (assemblyVersion == null)
                    return;

                switch (assemblyVersion.CompareTo(versionData.Version)) {
                    case > 0: // Running is newer
                        updateStatusLabel.ForeColor = Color.Teal;
                        updateStatusLabel.Text = "Vorabversion. Keine Updates verfügbar.";

                        updateButton.Text = "Zurückstufen";
                        updateButton.Tag = versionData;
                        updateButton.Visible = true;
                        updateButton.Enabled = true;
                        break;

                    case < 0: // Update available
                        updateStatusLabel.ForeColor = Color.Chocolate;
                        updateStatusLabel.Text = "Es ist ein update auf Version " + versionData.Version.ToString() + " verfügbar!";

                        updateButton.Text = "Aktualisieren";
                        updateButton.Tag = versionData;
                        updateButton.Visible = true;
                        updateButton.Enabled = true;
                        break;

                    default: // Equal
                        updateStatusLabel.ForeColor = Color.DarkGreen;
                        updateStatusLabel.Text = "Keine Updates verfügbar.";
                        updateButton.Visible = false;
                        break;
                }
            } else
                updateStatusLabel.Text = "> Error <";
        }

        #endregion Methods

        #region EventHandlers

        private async void updateButton_Click(object sender, EventArgs e) {
            if (sender is Button updateButton) {
                if (updateButton.Tag is VersionData versionData) {
                    FileInfo destinationFile = new(UpdateDownloadFilePath);

                    updateProgressBar.Value = 0;
                    updateProgressBar.Maximum = 200;

                    updateProgressBar.Visible = true;
                    updateProgressLabel.Visible = true;

                    Progress<float> downloadProgress = new((p) => {
                        if (updateProgressLabel.Text != "Herunterladen ...")
                            updateProgressLabel.Text = "Herunterladen ...";

                        if (p < 0) {
                            p *= -1;
                            updateProgressBar.SetState(ProgressBarExtensions.ProgressBarState.Error);
                        } else
                            updateProgressBar.SetState(ProgressBarExtensions.ProgressBarState.Normal);

                        int newValue = (int)(p * 100);
                        int max = updateProgressBar.Maximum / 2;
                        updateProgressBar.Value = newValue <= max ? newValue : max;
                    });

                    Progress<int> extractProgress = new((p) => {
                        if (updateProgressLabel.Text != "Extrahieren ...")
                            updateProgressLabel.Text = "Extrahieren ...";

                        if (p < 0) {
                            p *= -1;
                            updateProgressBar.SetState(ProgressBarExtensions.ProgressBarState.Error);
                        } else
                            updateProgressBar.SetState(ProgressBarExtensions.ProgressBarState.Normal);

                        int newValue = 100 + p;
                        int max = updateProgressBar.Maximum;
                        updateProgressBar.Value = newValue <= max ? newValue : max;
                    });

                    (bool Status, Exception? Error, DirectoryInfo? ExtractionDirectory) = await versionData.Download(destinationFile, downloadProgress, extractProgress, httpClient);

                    if (Status) {
                        updateButton.Text = "Anwenden ↪";
                        updateButton.Tag = ExtractionDirectory;
                        updateProgressLabel.Text = "Aktualisierung breit!";
                    } else {
                        updateButton.Enabled = false;
                        updateProgressLabel.Text = Error != null
                            ? Error.Message
                            : "Unbekannter Fehler!";
                    }
                } else if (updateButton.Tag is DirectoryInfo extractionDirectory) {
                }
            }
        }

        private async void updatesControl_Load(object sender, EventArgs e) {
            if (main.menuImageList_large.Images.ContainsKey(MenuImageKey))
                updatesImagePictureBox.Image = main.menuImageList_large.Images[MenuImageKey];

            await checkUpdates();
        }

        private async void updatesImagePictureBox_Click(object sender, EventArgs e)
            => await checkUpdates();

        #endregion EventHandlers
    }
}
