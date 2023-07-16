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

        public override string Description => "Bar Monitor PlugIn";
        public override string ImageKey => "device_tv";
        public override string Name => "Bar Monitor";

        #region Constructor

        public BarMonitorWKPlugIn(string directoryPath, WKManagerBase @base) : base(directoryPath, @base) {
        }

        #endregion Constructor

        #region Methods

        public override async Task Initialize()
            => await Task.CompletedTask;

        protected override void Dispose(bool disposing) {
        }

        #endregion Methods
    }
}
