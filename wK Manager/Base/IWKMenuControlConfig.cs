using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    public interface IWKMenuControlConfig
    {
        public abstract void InitializeDefault();
        public abstract string GetData(JsonSerializerSettings? jss = null);
        public abstract bool SetData(string json, JsonSerializerSettings? jss = null);
        public abstract Task<bool> Load(string filePath, bool initialize = true);
        public abstract Task<bool> Save(string filePath);
    }
}
