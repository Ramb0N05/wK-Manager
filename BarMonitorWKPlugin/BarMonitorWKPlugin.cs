using System;
using System.Reflection;
using wK_Manager.Base;

namespace wK_Manager.Plugins
{
    public class BarMonitorWKPlugin : WKPlugin
    {
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

        public override string Name => "Bar Monitor";
        public override string Description => "Bar Monitor Plugin";
        public override string ImageKey => "device-tv";

        public HttpClient HttpCli;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        public BarMonitorWKPlugin(object sender) : base(sender) { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

        public override async Task Initialize()
        {
            HttpCli = new HttpClient(new HttpClientHandler()
            {
                UseProxy = false
            });

            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            HttpCli.Dispose();
        }
    }
}