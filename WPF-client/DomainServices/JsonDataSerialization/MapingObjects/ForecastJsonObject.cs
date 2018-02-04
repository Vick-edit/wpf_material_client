using Newtonsoft.Json;

namespace WPF_client.DomainServices.JsonDataSerialization.MapingObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastJsonObject
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}