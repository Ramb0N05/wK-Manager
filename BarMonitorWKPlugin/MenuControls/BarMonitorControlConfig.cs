using Newtonsoft.Json;
using System.ComponentModel;
using wK_Manager.Base;

namespace wK_Manager.Plugins.MenuControls {
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class BarMonitorControlConfig : WKMenuControlConfig {
        [JsonIgnore]
        public new const string ConfigFileName = "bar_monitor.json";

        [JsonIgnore]
        public override string ConfigFilePath { get; set; } = IWKMenuControlConfig.AutoDetect_ConfigFilePath;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        [JsonProperty(Required = Required.AllowNull)]
        public string? DisplayTarget { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string LocalDiashowPath { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string RemoteDiashowPath { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(false)]
        public bool AutoObtainDiashow { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(typeof(uint), "5000")]
        public uint Interval { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(true)]
        public bool Repeat { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(false)]
        public bool Shuffle { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(false)]
        public bool LockedConfig { get; set; }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    }
}
