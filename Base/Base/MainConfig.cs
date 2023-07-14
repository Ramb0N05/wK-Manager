using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;

namespace wK_Manager.Base {
    [JsonObject(MemberSerialization.OptOut)]
    public class MainConfig : WKMenuControlConfig {

        [JsonIgnore]
        public override string ConfigFilePath { get; set; } = string.Empty;

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string StartupWindowName { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DefaultValue("")]
        public string UserConfigDirectory { get; set; }

        #region Constructor
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

        public MainConfig(string configFilePath) {
            ConfigFilePath = !configFilePath.IsNull() && File.Exists(configFilePath)
                ? configFilePath
                : throw new ArgumentException("Invalid config file path!", nameof(configFilePath));
        }

        [JsonConstructor]
        internal MainConfig() {
            if (ConfigProvider.Global?.ConfigFilePath.IsNull() == false)
                ConfigFilePath = ConfigProvider.Global.ConfigFilePath;
        }

#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        #endregion Constructor

        #region Methods

        public string GetUserConfigFilePath(string filename)
            => !filename.IsNull()
                ? (!UserConfigDirectory.IsNull()
                    ? Path.Combine(UserConfigDirectory, filename)
                    : filename)
                : string.Empty;

        #endregion Methods
    }
}
