using Newtonsoft.Json;

namespace wK_Manager.Base.Models {
    [JsonObject(MemberSerialization.OptOut)]
    public class VersionDataModel {
        [JsonProperty(Required = Required.Always)]
        public string Current { get; set; }

        [JsonProperty(Required = Required.Always)]

        public string DownloadURL { get; set; }

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        public VersionDataModel() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    }
}
