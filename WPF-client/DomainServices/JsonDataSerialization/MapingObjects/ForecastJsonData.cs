using Newtonsoft.Json;

namespace WPF_client.DomainServices.JsonDataSerialization.MapingObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastJsonData
    {
        public double AP { get; set; }

        public int day { get; set; }
        public int week { get; set; }
        public int weekday { get; set; }
        public int weekend { get; set; }
    }
}