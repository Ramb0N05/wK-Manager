using DotNet.Basics.SevenZip;
using DotNet.Basics.Sys;
using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using wK_Manager.Base.Extensions;
using wK_Manager.Base.Models;

namespace wK_Manager.Base {
    public class VersionData {
        public static readonly string FlallbackUpdateExtractDirectory = Path.Combine(Application.StartupPath, "update");

        public static readonly IEnumerable<string> AcceptedDiashowArchiveContentTypes = new List<string>
        {
            MimeMapping.KnownMimeTypes.Gz,
            MimeMapping.KnownMimeTypes.Cab,
            MimeMapping.KnownMimeTypes._7z,
            MimeMapping.KnownMimeTypes.Bz2,
            "application/x-compress",
            "application/x-lzma",
            MimeMapping.KnownMimeTypes.Tar,
            MimeMapping.KnownMimeTypes.Xz,
            MimeMapping.KnownMimeTypes.Zip
        };

        public Version Version { get; set; }
        public Uri Uri { get; set; }

        private readonly HttpClient? httpClient;

        #region Constructor
        public VersionData(Version version, Uri uri) : this(version, uri, null) { }
        public VersionData(Version version, Uri uri, HttpClient? httpClient) {
            Version = version;
            Uri = uri;
            this.httpClient = httpClient;
        }

        public VersionData(VersionDataModel model) : this(model, null) { }
        public VersionData(VersionDataModel model, HttpClient? httpClient) {
            this.httpClient = httpClient;

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
        #endregion

        #region Methods
        public async Task<(bool Status, Exception? Error, DirectoryInfo? ExtractionDirectory)> Download(FileInfo destinationFile) => await Download(destinationFile, null, null, null);
        public async Task<(bool Status, Exception? Error, DirectoryInfo? ExtractionDirectory)> Download(FileInfo destinationFile, IProgress<float>? downloadProgress = null) => await Download(destinationFile, downloadProgress, null, null);
        public async Task<(bool Status, Exception? Error, DirectoryInfo? ExtractionDirectory)> Download(FileInfo destinationFile, IProgress<float>? downloadProgress = null, IProgress<int>? extractProgress = null) => await Download(destinationFile, downloadProgress, extractProgress, null);
        public async Task<(bool Status, Exception? Error, DirectoryInfo? ExtractionDirectory)> Download(FileInfo destinationFile, IProgress<float>? downloadProgress = null, IProgress<int>? extractProgress = null, HttpClient? httpClient = null) {
            httpClient ??= this.httpClient ?? new(new HttpClientHandler() { UseProxy = false });
            using HttpRequestMessage request = new(HttpMethod.Get, Uri);
            DirectoryInfo extractDir = destinationFile.Directory ?? new DirectoryInfo(FlallbackUpdateExtractDirectory);

            try {
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (response.IsSuccessStatusCode) {
                    if (response.Content.Headers.ContentType?.MediaType != null) {
                        if (!AcceptedDiashowArchiveContentTypes.Contains(response.Content.Headers.ContentType.MediaType))
                            return new() {
                                Error = new Exception("Invalid content type! (" + response.Content.Headers.ContentType.MediaType + ")"),
                                ExtractionDirectory = null,
                                Status = false
                            };
                    } else
                        return new() {
                            Error = new Exception("Could not get content type!"),
                            ExtractionDirectory = null,
                            Status = false
                        };
                } else
                    return new() {
                        Error = new Exception("Could not download update!" + Environment.NewLine + "(" +
                                              ((int)response.StatusCode).ToString() + " - " + response.StatusCode.ToString() +
                                              ")"),
                        ExtractionDirectory = null,
                        Status = false
                    };
            } catch (Exception ex) {
                return new() {
                    Error = ex,
                    ExtractionDirectory = null,
                    Status = false
                };
            }

            try {
                FileStream fStream = new(destinationFile.FullName, FileMode.Create);
                await httpClient.DownloadAsync(Uri, fStream, downloadProgress);
                await fStream.FlushAsync();
                fStream.Close();
            } catch (Exception ex) {
                downloadProgress?.Report(-50);

                return new() {
                    Error = ex,
                    ExtractionDirectory = null,
                    Status = false
                };
            }

            int currentExtractProgressValue = 0;
            try {
                if (extractDir.Exists)
                    extractDir.Delete();

                currentExtractProgressValue = 25;
                extractProgress?.Report(currentExtractProgressValue);

                if (destinationFile.Exists) {
                    int result = await Task.Run(() => {
                        SevenZipExe sevenZipExe = new();
                        return sevenZipExe.ExtractToDirectory(destinationFile.FullName, extractDir.FullName);
                    });

                    currentExtractProgressValue = 90;
                    extractProgress?.Report(currentExtractProgressValue);

                    if (result == 0) {
                        destinationFile.Delete();
                        currentExtractProgressValue = 100;
                        extractProgress?.Report(currentExtractProgressValue);
                        return new() {
                            Error = null,
                            ExtractionDirectory = extractDir,
                            Status = true
                        };
                    } else {
                        currentExtractProgressValue = 99;
                        extractProgress?.Report(- currentExtractProgressValue);

                        return new() {
                            Error = new Exception("Extracion finished with exit code \"" + result.ToString() + "\""),
                            ExtractionDirectory = null,
                            Status = false
                        };
                    }
                } else {
                    currentExtractProgressValue = 11;
                    extractProgress?.Report(- currentExtractProgressValue);

                    return new() {
                        Error = new Exception("Downloaded file does not exist!"),
                        ExtractionDirectory = null,
                        Status = false
                    };
                }
            } catch (Exception ex) {
                extractProgress?.Report(- currentExtractProgressValue);

                return new() {
                    Error = ex,
                    ExtractionDirectory = null,
                    Status = false
                };
            }
        }

        public static async Task<VersionData?> GetCurrent(string versionFileUrl, HttpClient? httpClient) {
            if (!Uri.IsWellFormedUriString(versionFileUrl, UriKind.Absolute))
                return null;

            Uri updateManifestUri = new(versionFileUrl);
            using HttpRequestMessage request = new(HttpMethod.Get, updateManifestUri);
            bool newHttpClient = httpClient == null;
            httpClient ??= new HttpClient(new HttpClientHandler() { UseProxy = false });

            try {
                using HttpResponseMessage respone = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                MediaTypeHeaderValue? contentType = respone.Content.Headers.ContentType;

                if (!respone.IsSuccessStatusCode || contentType == null || contentType.MediaType != MimeMapping.KnownMimeTypes.Json)
                    return null;

                Memory<byte> buffer = new(Array.Empty<byte>());
                using StreamReader versionDataStream = new(await respone.Content.ReadAsStreamAsync());
                string versionDataJson = await versionDataStream.ReadToEndAsync();
                VersionDataModel? versionDataModel = JsonConvert.DeserializeObject<VersionDataModel>(versionDataJson);

                if (newHttpClient)
                    httpClient.Dispose();

                return versionDataModel != null ? new VersionData(versionDataModel) : null;
            } catch (Exception) {
                if (newHttpClient)
                    httpClient.Dispose();

                return null;
            }
        }
        #endregion

        #region ModelMethods
        public static VersionData FromModel(VersionDataModel model)
            => new(model);

        public VersionDataModel ToModel()
            => new() {
                Current = Version.ToString(),
                DownloadURL = Uri.ToString()
            };
        #endregion
    }
}
