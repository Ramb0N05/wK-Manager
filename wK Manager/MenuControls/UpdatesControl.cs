using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;
using wK_Manager.Base.DataModels;

namespace wK_Manager.MenuControls {
    public partial class UpdatesControl : WKMenuControl {
        public const string VersionFileContentType = MimeMapping.KnownMimeTypes.Json;
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
        private async Task<VersionDataModel?> getCurrentVersion() {
            if (!Uri.IsWellFormedUriString(updateManifestURL, UriKind.Absolute))
                return null;

            Uri updateManifestUri = new(updateManifestURL);
            using HttpRequestMessage request = new(HttpMethod.Get, updateManifestUri);

            try {
                using HttpResponseMessage respone = await httpCli.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                MediaTypeHeaderValue? contentType = respone.Content.Headers.ContentType;

                if (contentType == null || contentType.MediaType != VersionFileContentType)
                    return null;

                Memory<byte> buffer = new(Array.Empty<byte>());
                using StreamReader versionDataStream = new(await respone.Content.ReadAsStreamAsync());
                string versionDataJson = await versionDataStream.ReadToEndAsync();

                return JsonConvert.DeserializeObject<VersionDataModel>(versionDataJson);
            } catch (Exception) {
                return null;
            }
        }

        private (Version Version, Uri Uri)? parseVersionData(VersionDataModel? versionData) {
            if (versionData != null && Version.TryParse(versionData.Current, out Version? version) && version != null) {
                UriCreationOptions uco = new() { DangerousDisablePathAndQueryCanonicalization = false };
                
                return Uri.TryCreate(versionData.DownloadURL, uco, out Uri? uri) && uri != null
                    ? new() {
                        Version = version,
                        Uri = uri
                    }
                    : null;
            } else
                return null;
        }

        public new void Dispose() {
            httpCli.Dispose();
            base.Dispose();
        }
        #endregion

        #region EventHandlers
        private async void updatesControl_Load(object sender, EventArgs e) {
            if (main.menuImageList_large.Images.ContainsKey(MenuImageKey))
                updatesImagePictureBox.Image = main.menuImageList_large.Images[MenuImageKey];

            (Version Version, Uri Uri)? version = parseVersionData(await getCurrentVersion());

            if (version.HasValue) {
                updateStatusLabel.Text = version.Value.Version.ToString() + Environment.NewLine;
                updateStatusLabel.Text += version.Value.Uri.AbsoluteUri;
            } else
                updateStatusLabel.Text = "> Error <";
        }
        #endregion
    }
}
