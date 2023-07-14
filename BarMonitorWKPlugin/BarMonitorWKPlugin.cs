using wK_Manager.Base;

namespace wK_Manager.PlugIns {

    public class BarMonitorWKPlugIn : WKPlugIn {

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

        public static readonly IEnumerable<string> AcceptedDiashowContentTypes = new List<string>
        {
            MimeMapping.KnownMimeTypes.Bmp,
            MimeMapping.KnownMimeTypes.Gif,
            MimeMapping.KnownMimeTypes.Jpeg,
            MimeMapping.KnownMimeTypes.Png,
            "image/x-png"
        };

        public HttpClient HttpClient { get; private set; }
        public override string Description => "Bar Monitor PlugIn";
        public override string ImageKey => "device-tv";
        public override string Name => "Bar Monitor";

        #region Constructor
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

        public BarMonitorWKPlugIn(string directoryPath, object sender) : base(directoryPath, sender) {
        }

#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        #endregion Constructor

        #region Methods

        public override void Dispose()
            => HttpClient.Dispose();

        public override async Task Initialize() {
            HttpClient = new HttpClient(new HttpClientHandler() {
                UseProxy = false
            });

            await Task.CompletedTask;
        }

        #endregion Methods
    }
}
