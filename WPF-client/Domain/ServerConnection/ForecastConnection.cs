using System.Collections.Generic;
using RestSharp;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.Utilities;

namespace WPF_client.Domain.ServerConnection
{
    /// <summary> Класс подключения к серверу для получения данных погноза </summary>
    public class ForecastConnection : IForecastConnection
    {
        private readonly IJsonSingleObjectDeserializer<ForecastBlock> _jsonDeserializer;
        private readonly RestClient _restClient;
        private readonly ForecastSize _forecastSize;

        public ForecastConnection(IJsonSingleObjectDeserializer<ForecastBlock> jsonDeserializer, ForecastSize forecastSize)
        {
            _forecastSize = forecastSize;
            _jsonDeserializer = jsonDeserializer;
            _restClient = new RestClient(ServerUrl.ServerName);
        }


        /// <summary> Метод, который получает свежую порцию данных с сервера </summary>
        public ForecastBlock GetForecasts()
        {
            var jsonData = GetJsonForecast();
            var forecastsData = _jsonDeserializer.Deserialize(jsonData);
            return forecastsData;
        }


        //Получить строку с прогнозами от сервера
        private string GetJsonForecast()
        {
            /*
            var randomise = new Random();
            var newValue = randomise.Next(0, 2);
            if (newValue >= 1)
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                var rootFolder = Directory.GetParent(exePath).Parent.Parent.Parent.FullName;
                var jsonFile = Path.Combine(rootFolder, "WPF-client.Test", "TestData", "archive.json");

                var jsonData = File.ReadAllText(jsonFile);
                return jsonData;
            }

            throw  new ConnectionException("local");
            */

            var request = new RestRequest(ServerUrl.ForecastsUris[_forecastSize], Method.GET);
            request.AddUrlSegment("id", Session.Instance.ActiveForecastObjectId.ToString());
            var response = _restClient.Execute(request);
            return response.Content;
        }
    }
}