using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                var jsonElements = JsonConvert.DeserializeObject<Dictionary<string, ForecastJsonData>>(jsonString);
                if (jsonElements == null || jsonElements.Count == 0)
                    throw new JsonException("Не удалось найти объекты прогноза в JSON");

                //Из каждого элемента словаря создаем объект
                var forecasts = new List<Forecast>();
                foreach (KeyValuePair<string, ForecastJsonData> jsonElement in jsonElements)
                {
                    var forecastDate = new DateTime(1970, 1, 1, 0, 0, 0);
                    forecastDate = forecastDate.AddMilliseconds(long.Parse(jsonElement.Key));

                    var forecastData = jsonElement.Value;
                    forecasts.Add(new Forecast
                    {
                        ForecastPower = forecastData.AP,
                        ForecastTime = forecastDate,

                        DaySerialNumber = forecastData.day,
                        WeekSerialNumber = forecastData.week,
                        DayOfWeekNumber = forecastData.weekday,
                        IsItWeekend = forecastData.weekend == 1,
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