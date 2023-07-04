using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wK_Manager.Base;

namespace wK_Manager.MenuControls
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class BarMonitorControlConfig : WKMenuControlConfig
    {
        [JsonIgnore]
        public const string ConfigFileName = "bar_monitor.json";

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        [JsonProperty(Required = Required.Always)]
        [DefaultValue(0)]
        public int MonitorSourceID { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(typeof(uint), "0")]
        public uint MonitorTargetID { get; set; }

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
        [DefaultValue("")]
        public string VLCPath { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue(false)]
        public bool LockedConifg { get; set; }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    }
}
