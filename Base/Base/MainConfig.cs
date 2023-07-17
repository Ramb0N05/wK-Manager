using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;
using wK_Manager.Base.Providers;

namespace wK_Manager.Base {

    [JsonObject(MemberSerialization.OptOut)]
    public class MainConfig : WKConfig {

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
            ConfigFilePath = !configFilePath.IsNull()
                ? configFilePath
                : throw new ArgumentException("Empty config file path!", nameof(configFilePath));
        }

        [JsonConstructor]
        internal MainConfig() {
        }

#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        #endregion Constructor

        #region Methods

        public string GetUserConfigFilePath(string filename) {
            if (!filename.IsNull())
                return !UserConfigDirectory.IsNull() ? Path.Combine(UserConfigDirectory, filename) : filename;

            return string.Empty;
        }

        #endregion Methods
    }
}
