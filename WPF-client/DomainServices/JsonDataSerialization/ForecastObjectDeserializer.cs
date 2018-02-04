using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    public class ForecastObjectDeserializer : IJsonDeserializer<ForecastJsonObject>
    {
        public IList<ForecastJsonObject> Deserialize(string jsonString)
        {
            try
            {
                //Вытаскиваем словарь объектов
                var jsonElements = JsonConvert.DeserializeObject<List<ForecastJsonObject>>(jsonString);
                if (jsonElements == null || jsonElements.Count == 0)
                    throw new JsonException("Не удалось найти объекты прогнозирования в JSON");
                return jsonElements;
            }
            catch (Exception e)
            {
                throw new JsonException("Не удалось получить объекты прогнозирования - некорректный формат JSON", e);
            }
        }
    }
}