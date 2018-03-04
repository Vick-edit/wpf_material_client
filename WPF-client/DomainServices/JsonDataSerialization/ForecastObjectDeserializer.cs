using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    /// <summary> Класс, извлекающий из JSON данные об объектах прогнозирования <see cref="ForecastObject"/> </summary>
    public class ForecastObjectDeserializer : IJsonDeserializer<ForecastObject>
    {
        /// <summary> Вытащить данные об объектх прогнозирвоания <see cref="ForecastObject"/> </summary>
        /// <param name="jsonString">Строка, содержащая JSON объекты</param>
        /// <returns>Список <see cref="ForecastObject"/>, содержащийся во входном JSON</returns>
        public IList<ForecastObject> Deserialize(string jsonString)
        {
            try
            {
                //Вытаскиваем словарь объектов
                var jsonElements = JsonConvert.DeserializeObject<List<ForecastJsonObject>>(jsonString);
                if (jsonElements == null || jsonElements.Count == 0)
                    throw new JsonException("Не удалось найти объекты прогнозирования в JSON");
                //Преобразуем данные
                var forecastObjects = new List<ForecastObject>();
                foreach (var forecastJsonObject in jsonElements)
                {
                    var nextForecastObject = new ForecastObject()
                    {
                        Id = forecastJsonObject.id,
                        Name = forecastJsonObject.name,
                    };
                    forecastObjects.Add(nextForecastObject);
                }
                return forecastObjects;
            }
            catch (Exception e)
            {
                throw new JsonException("Не удалось получить объекты прогнозирования - некорректный формат JSON", e);
            }
        }
    }
}