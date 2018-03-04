using System;
using Newtonsoft.Json;

namespace WPF_client.DomainServices.JsonDataSerialization.MapingObjects
{
    /// <summary> Json прототип данных о прогнозе/реальном потреблении <see cref="Forecast"/> </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastJsonData
    {
        public double ap { get; set; }
        public DateTime date { get; set; }
        public bool is_predict { get; set; }
    }
}