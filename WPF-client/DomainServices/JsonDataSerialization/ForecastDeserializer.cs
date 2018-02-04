using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    public class ForecastDeserializer : IJsonDeserializer<Forecast>
    {
        public IList<Forecast> Deserialize(string jsonString)
        {
            try
            {
                //Вытаскиваем словарь прогнозов
                var jsonDeserializeSettings = new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-dd\\THH:mm:ss\\Z"
                };
                var jsonElements = JsonConvert.DeserializeObject<List<ForecastJsonData>>(jsonString);
                if (jsonElements == null || jsonElements.Count == 0)
                    throw new JsonException("Не удалось найти ни одного объекта прогноза в JSON");

                //Из каждого элемента словаря создаем объект
                var forecasts = new List<Forecast>();
                foreach (var forecastData in jsonElements)
                {
                    forecasts.Add(new Forecast
                    {
                        ForecastPower = forecastData.ap,
                        ForecastTime = forecastData.time,
                    });
                }

                return forecasts;
            }
            catch (Exception e)
            {
                throw new JsonException("Не удалось получить объекты прогноза - некорректный формат JSON", e);
            }
        }
    }
}