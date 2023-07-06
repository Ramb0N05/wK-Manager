using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;

namespace wK_Manager.Base
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MainConfig : WKMenuControlConfig
    {
        [JsonIgnore]
        public override string ConfigFilePath { get; set; } = string.Empty;

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

        [JsonConstructor]
        internal MainConfig() {
            if (ConfigProvider.Global != null && !ConfigProvider.Global.ConfigFilePath.IsNull())
                ConfigFilePath = ConfigProvider.Global.ConfigFilePath;
        }

        public MainConfig(string configFilePath)
        {
            ConfigFilePath = !configFilePath.IsNull() && File.Exists(configFilePath)
                ? configFilePath
                : throw new ArgumentException("Invalid config file path!", nameof(configFilePath));
        }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

        public string GetUserConfigFilePath(string filename)
            => !filename.IsNull()
                ? (!UserConfigDirectory.IsNull()
                    ? Path.Combine(UserConfigDirectory, filename)
                    : filename)
                : string.Empty;
    }
}
