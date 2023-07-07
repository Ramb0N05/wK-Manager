using Newtonsoft.Json;
using SharpRambo.ExtensionsLib;
using System.ComponentModel;
using System.Reflection;

namespace wK_Manager.Base {
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class WKMenuControlConfig : IWKMenuControlConfig {
        [JsonIgnore]
        public const string ConfigFileName = IWKMenuControlConfig.ConfigFileName;

        [JsonIgnore]
        public abstract string ConfigFilePath { get; set; }

        [JsonIgnore]
        public JsonSerializerSettings GlobalJsonSerializerSettings { get; set; } = new JsonSerializerSettings() {
            Formatting = Formatting.Indented
        };

        public WKMenuControlConfig() {
            if (ConfigProvider.Global != null) {
                Type baseType = GetType();
                FieldInfo? cfnField = baseType.GetField(nameof(ConfigFileName));
                PropertyInfo? cfpProperty = baseType.GetProperty(nameof(ConfigFilePath));

                if (cfnField != null && cfpProperty != null) {
                    string cfnValue = (string?)cfnField.GetValue(null) ?? string.Empty;
                    string cfpValue = (string?)cfpProperty.GetValue(this) ?? string.Empty;

                    if (!cfnValue.IsNull() && cfnValue != IWKMenuControlConfig.ConfigFileName && cfpValue == IWKMenuControlConfig.AutoDetect_ConfigFilePath)
                        cfpProperty.SetValue(this, ConfigProvider.Global.GetUserConfigFilePath(cfnValue));
                }
            }
        }

        public virtual void InitializeDefault() {
            PropertyInfo[] props = GetType().GetProperties();
            foreach (PropertyInfo prop in props) {
                DefaultValueAttribute? d = prop.GetCustomAttribute<DefaultValueAttribute>();

                if (d != null)
                    prop.SetValue(this, d.Value);
                else if (prop.IsNullable())
                    prop.SetValue(this, null);
            }
        }

        public virtual string GetData(JsonSerializerSettings? jss = null)
            => JsonConvert.SerializeObject(this, GetType(), jss);

        public virtual bool SetData(string json, JsonSerializerSettings? jss = null) {
            object? data = JsonConvert.DeserializeObject(json, GetType(), jss);
            return data != null && data is WKMenuControlConfig dataOfType && SetData(dataOfType);
        }

        public virtual bool SetData(IWKMenuControlConfig? configObject) => SetData(configObject as WKMenuControlConfig);
        public virtual bool SetData(WKMenuControlConfig? configObject) {
            if (configObject != null) {
                Type dataType = configObject.GetType();
                Type targetType = GetType();
                PropertyInfo[] dataProperties = dataType.GetProperties();
                bool dataChanged = false;

                foreach (PropertyInfo dataProperty in dataProperties) {
                    if (dataProperty.GetCustomAttribute(typeof(JsonPropertyAttribute), true) != null) {
                        PropertyInfo? targetProperty = targetType.GetProperty(dataProperty.Name, dataProperty.PropertyType);

                        if (targetProperty != null && targetProperty.CanWrite) {
                            targetProperty.SetValue(this, dataProperty.GetValue(configObject));
                            dataChanged = true;
                        }
                    }
                }

                return dataChanged;
            } else
                return false;
        }

        public virtual async Task<bool> Load(bool initialize = true) {
            string filePath = getCurrentConfigFilePath() ?? string.Empty;

            if (filePath.IsNull())
                return false;

            if (initialize && !File.Exists(filePath)) {
                InitializeDefault();
                return await Save();
            }

            if (File.Exists(filePath)) {
                string json = await File.ReadAllTextAsync(filePath);
                return SetData(json, GlobalJsonSerializerSettings);
            } else
                return false;
        }

        public virtual async Task<bool> Save(bool makeDirectory = true) {
            string filePath = getCurrentConfigFilePath() ?? string.Empty;

            if (filePath.IsNull())
                return false;

            DirectoryInfo fileDir = new FileInfo(filePath).Directory ?? throw new NullReferenceException(nameof(filePath));

            if (makeDirectory && !fileDir.Exists)
                fileDir.CreateAnyway();

            await File.WriteAllTextAsync(filePath, GetData(GlobalJsonSerializerSettings));
            return true;
        }

        private string? getCurrentConfigFilePath() {
            Type baseType = GetType();
            PropertyInfo? cfpProperty = baseType.GetProperty(nameof(ConfigFilePath));

            return (string?)cfpProperty?.GetValue(this);
        }
    }
}
