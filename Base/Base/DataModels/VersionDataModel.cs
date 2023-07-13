using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base.DataModels {
    [JsonObject(MemberSerialization.OptOut)]
    public class VersionDataModel {
        [JsonProperty(Required = Required.Always)]
        public string Current { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string DownloadURL { get; set; }
    }
}
