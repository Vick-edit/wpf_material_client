using Newtonsoft.Json;
using WPF_client.Domain.DomainModels;

namespace WPF_client.DomainServices.JsonDataSerialization.MapingObjects
{
    /// <summary> Json прототип объекта прогнозирования <see cref="ForecastObject"/> </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ForecastJsonObject
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}