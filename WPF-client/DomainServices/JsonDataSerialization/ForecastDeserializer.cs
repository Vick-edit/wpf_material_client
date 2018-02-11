using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;

namespace WPF_client.DomainServices.JsonDataSerialization
{
    public class ForecastDeserializer : IJsonSingleObjectDeserializer<ForecastBlock>
    {
        public ForecastBlock Deserialize(string jsonString)
        {
            try
            {
                var jsonDeserializeSettings = new JsonSerializerSettings()
                {
                    DateFormatString = "dd.MM.yyyy"
                };

                //Вытаскиваем словарь объектов
                var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString, jsonDeserializeSettings);
                if (jsonDict == null || jsonDict.Count != 2)
                    throw new JsonException("Не удалось найти структуру данных прогнозирования");

                //Вытаскиваем потребление
                double consumption;
                var style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
                var culture = CultureInfo.InvariantCulture;
                if (!double.TryParse(jsonDict["consumption"].ToString(), style, culture, out consumption))
                    throw new JsonException("Не удалось найти сведения о потреблении в JSON");

                //Вытаскиваем прогнозы
                var jsonElements = (jsonDict["points"] as JArray)?.ToObject<List<ForecastJsonData>>();
                if (jsonElements == null || !jsonElements.Any())
                    throw new JsonException("Не удалось найти ни одного объекта прогноза в JSON");

                //Из каждого элемента словаря создаем объект
                var forecasts = new List<Forecast>();
                foreach (var forecastData in jsonElements)
                {
                    forecasts.Add(new Forecast
                    {
                        ForecastPower = forecastData.ap,
                        ForecastTime = forecastData.date,
                        IsForecast = forecastData.is_predict
                    });
                }

                var forecastBlock = new ForecastBlock()
                {
                    Forecasts = forecasts.OrderBy(x => x.ForecastTime).ToList(),
                    Consumption = consumption
                };
                return forecastBlock;
            }
            catch (Exception e)
            {
                throw new JsonException("Не удалось получить объекты прогноза - некорректный формат JSON", e);
            }
        }
    }
}