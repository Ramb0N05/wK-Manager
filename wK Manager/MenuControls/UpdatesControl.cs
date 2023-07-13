using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;
using wK_Manager.Base.Models;

namespace wK_Manager.MenuControls {
    public partial class UpdatesControl : WKMenuControl {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly HttpClient httpCli = new(new HttpClientHandler() { UseProxy = false });
        private MainForm main { get; set; }
        private readonly string updateManifestURL = Properties.Settings.Default.updateManifestURL;

        #region Constructor
        public UpdatesControl(object sender) : base(sender) {
            InitializeComponent();
            main = sender as MainForm ?? throw new ArgumentNullException(nameof(sender));
        }
        #endregion

        #region Methods
        public new void Dispose() {
            httpCli.Dispose();
            base.Dispose();
        }

        private async Task checkUpdates() {
            updateStatusLabel.ForeColor = SystemColors.ControlText;
            updateStatusLabel.Text = "Prüfe auf updates ...";
            VersionData? versionData = await VersionData.GetCurrent(updateManifestURL, httpCli);

            if (versionData != null) {
                Version? assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

                if (assemblyVersion == null)
                    return;

                switch (assemblyVersion.CompareTo(versionData.Version)) {
                    case > 0: // Running is newer
                        updateStatusLabel.ForeColor = Color.Teal;
                        updateStatusLabel.Text = "Vorabversion. Keine Updates verfügbar.";
                        break;

                    case < 0: // Update availabile
                        updateStatusLabel.ForeColor = Color.Chocolate;
                        updateStatusLabel.Text = "Es ist ein update auf Version " + versionData.Version.ToString() + " verfügbar!";
                        break;

                    case 0:
                    default: // Equal
                        updateStatusLabel.ForeColor = Color.DarkGreen;
                        updateStatusLabel.Text = "Keine Updates verfügbar.";
                        break;
                }
            } else
                updateStatusLabel.Text = "> Error <";
        }
        #endregion

        #region EventHandlers
        private async void updatesControl_Load(object sender, EventArgs e) {
            if (main.menuImageList_large.Images.ContainsKey(MenuImageKey))
                updatesImagePictureBox.Image = main.menuImageList_large.Images[MenuImageKey];

            await checkUpdates();
        }
        #endregion
    }
}
