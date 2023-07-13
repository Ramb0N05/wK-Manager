using Newtonsoft.Json;
using System.Net.Http.Headers;
using wK_Manager.Base.Models;

namespace wK_Manager.Base {
    public class VersionData {
        public Version Version { get; set; }
        public Uri Uri { get; set; }

        public VersionData(Version version, Uri uri) {
            Version = version;
            Uri = uri;
        }

        public VersionData(VersionDataModel model) {
            if (model != null && Version.TryParse(model.Current, out Version? version) && version != null) {
                UriCreationOptions uco = new() { DangerousDisablePathAndQueryCanonicalization = false };

                if (Uri.TryCreate(model.DownloadURL, uco, out Uri? uri) && uri != null) {
                    Version = version;
                    Uri = uri;
                } else
                    throw new ArgumentException("Property \"" + nameof(model.DownloadURL) + "\" is invalid or empty!", nameof(model));
            } else
                throw new ArgumentException("Property \"" + nameof(model.Current) + "\" is invalid or empty!", nameof(model));
        }

        public static async Task<VersionData?> GetCurrent(string versionFileUrl, HttpClient httpClient) {
            if (!Uri.IsWellFormedUriString(versionFileUrl, UriKind.Absolute))
                return null;

            Uri updateManifestUri = new(versionFileUrl);
            using HttpRequestMessage request = new(HttpMethod.Get, updateManifestUri);

            try {
                using HttpResponseMessage respone = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                MediaTypeHeaderValue? contentType = respone.Content.Headers.ContentType;

                if (!respone.IsSuccessStatusCode || contentType == null || contentType.MediaType != MimeMapping.KnownMimeTypes.Json)
                    return null;

                Memory<byte> buffer = new(Array.Empty<byte>());
                using StreamReader versionDataStream = new(await respone.Content.ReadAsStreamAsync());
                string versionDataJson = await versionDataStream.ReadToEndAsync();
                VersionDataModel? versionDataModel = JsonConvert.DeserializeObject<VersionDataModel>(versionDataJson);

                return versionDataModel != null ? new VersionData(versionDataModel) : null;
            } catch (Exception) {
                return null;
            }
        }

        public static VersionData FromModel(VersionDataModel model)
            => new(model);

        public VersionDataModel ToModel()
            => new() {
                Current = Version.ToString(),
                DownloadURL = Uri.ToString()
            };
    }
}
