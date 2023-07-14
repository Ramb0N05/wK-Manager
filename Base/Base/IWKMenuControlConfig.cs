using Newtonsoft.Json;
namespace wK_Manager.Base {
    public interface IWKMenuControlConfig {
        public const string AutoDetect_ConfigFilePath = "<cfp:auto_detect>";
        public const string ConfigFileName = "<cfn:undefined>";

        public abstract string ConfigFilePath { get; set; }
        public abstract JsonSerializerSettings GlobalJsonSerializerSettings { get; set; }

        public abstract void InitializeDefault();
        public abstract string GetData(JsonSerializerSettings? jsonSerializerSettings = null);
        public abstract bool SetData(string json, JsonSerializerSettings? jsonSerializerSettings = null);
        public abstract Task<bool> Load(bool initialize = true);
        public abstract Task<bool> Save(bool makeDirectory = true);
    }
}
