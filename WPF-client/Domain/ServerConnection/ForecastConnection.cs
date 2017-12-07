using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization;

namespace WPF_client.Domain.ServerConnection
{
    /// <summary> Класс подключения к серверу для получения данных погноза </summary>
    public class ForecastConnection : IForecastConnection
    {
        private readonly IJsonDeserializer<Forecast> _jsonDeserializer;

        public ForecastConnection(IJsonDeserializer<Forecast> jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
        }


        /// <summary> Метод, который получает свежую порцию данных с сервера </summary>
        public IList<Forecast> GetForecasts()
        {

            var jsonData = GetJsonForecast();
            var forecastsData = _jsonDeserializer.Deserialize(jsonData);
            return forecastsData;
        }


        //Получить строку с прогнозами от сервера
        private string GetJsonForecast()
        {
#if DEBUG
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var rootFolder = Directory.GetParent(exePath).Parent.Parent.Parent.FullName;
            var jsonFile = Path.Combine(rootFolder, "WPF-client.Test", "TestData", "archive.json");

            var jsonData = File.ReadAllText(jsonFile);
            return jsonData;
#else
            throw new System.NotImplementedException();
#endif
        }
    }
}