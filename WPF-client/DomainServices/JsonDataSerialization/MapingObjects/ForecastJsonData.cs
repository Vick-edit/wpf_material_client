using System;
using Newtonsoft.Json;

namespace WPF_client.DomainServices.JsonDataSerialization.MapingObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastJsonData
    {
        public long id { get; set; }
        public double ap { get; set; }
        public DateTime time { get; set; }
        public bool is_predict { get; set; }

        /*
        public int day { get; set; }
        public int week { get; set; }
        public int weekday { get; set; }
        public int weekend { get; set; }
        */
    }
}