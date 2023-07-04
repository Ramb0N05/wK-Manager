using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class WKMenuControlConfig : IWKMenuControlConfig
    {
        [JsonIgnore]
        public JsonSerializerSettings GlobalJsonSerializerSettings { get; set; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public virtual void InitializeDefault()
        {
            PropertyInfo[] props = GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                DefaultValueAttribute? d = prop.GetCustomAttribute<DefaultValueAttribute>();

                if (d != null)
                    prop.SetValue(this, d.Value);
                else if (prop.IsNullable())
                    prop.SetValue(this, null);
            }
        }

        public virtual string GetData(JsonSerializerSettings? jss = null)
            => JsonConvert.SerializeObject(this, GetType(), jss);

        public virtual bool SetData(string json, JsonSerializerSettings? jss = null)
        {
            object? data = JsonConvert.DeserializeObject(json, GetType(), jss);

            if (data != null)
            {
                Type dataType = data.GetType();
                Type targetType = GetType();
                PropertyInfo[] dataProperties = dataType.GetProperties();
                bool dataChanged = false;

                foreach (PropertyInfo dataProperty in dataProperties)
                {
                    PropertyInfo? targetProperty = targetType.GetProperty(dataProperty.Name, dataProperty.PropertyType);

                    if (targetProperty != null)
                    {
                        targetProperty.SetValue(this, dataProperty.GetValue(data));
                        dataChanged = true;
                    }
                }

                return dataChanged;
            } else
                return false;
        }

        public virtual async Task<bool> Load(string filePath, bool initialize = true)
        {
            if (initialize && !File.Exists(filePath))
            {
                InitializeDefault();
                return await Save(filePath);
            }

            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                return SetData(json, GlobalJsonSerializerSettings);
            }
            else
                return false;
        }

        public virtual async Task<bool> Save(string filePath)
        {
            await File.WriteAllTextAsync(filePath, GetData(GlobalJsonSerializerSettings));
            return true;
        }
    }
}
