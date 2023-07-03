using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MainConfig : GenericWKMenuControlConfig
    {
        [JsonIgnore]
        public static readonly string ConfigFilePath = Path.Combine(Application.StartupPath, Properties.Settings.Default.mainConfigName);

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string SevenZipPath { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string StartupWindowName { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string UserConfigDirectory { get; set; }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

        public string GetUserConfigFilePath(string filename)
            => !filename.IsNull()
                ? (!UserConfigDirectory.IsNull()
                    ? Path.Combine(UserConfigDirectory, filename)
                    : filename)
                : string.Empty;
    }
}
