using System;
using System.IO;
using RestSharp;
using WPF_client.Domain;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.Exceptions;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.ServerConnection;
using WPF_client.Utilities;

namespace WPF_client.DomainServices.ServerDataProviders
{
    /// <summary> Класс подключения к серверу для получения данных погноза </summary>
    public class GetForecastBlock : IGetSingleObjectRequest<ForecastBlock>
    {
        private readonly IJsonSingleObjectDeserializer<ForecastBlock> _jsonDeserializer;
        private readonly RestClient _restClient;
        private readonly ForecastSize _forecastSize;


        public GetForecastBlock(IJsonSingleObjectDeserializer<ForecastBlock> jsonDeserializer, ForecastSize forecastSize)
        {
            _forecastSize = forecastSize;
            _jsonDeserializer = jsonDeserializer;
            _restClient = new RestClient(ServerUrl.ServerName);
        }

        public GetForecastBlock(IJsonSingleObjectDeserializer<ForecastBlock> jsonDeserializer, ForecastSize forecastSize, string serverName)
        {
            _forecastSize = forecastSize;
            _jsonDeserializer = jsonDeserializer;
            _restClient = new RestClient(serverName);
        }


        /// <summary> Получить список список данные о прогнозирвоании с сервера </summary>
        /// <returns>Данные о прогнозе в формате <see cref="ForecastBlock"/></returns>
        public ForecastBlock GetDataFromServer()
        {
            string jsonData;
#if DEBUG
            var randomise = new Random();
            var newValue = randomise.Next(0, 2);
            if (newValue >= -1)
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                var rootFolder = Directory.GetParent(exePath).Parent.Parent.Parent.FullName;
                var jsonFile = Path.Combine(rootFolder, "WPF-client.Test", "TestData", "archive.json");

                jsonData = File.ReadAllText(jsonFile);
            }
            else
            {
                throw new ConnectionException("local");
            }
#else
            var request = new RestRequest(ServerUrl.ForecastsUris[_forecastSize], Method.GET);
            request.AddUrlSegment("id", Session.Instance.ActiveForecastObjectId.ToString());
            var response = _restClient.Execute(request);
            if(response.ErrorException != null)
                throw new ConnectionException(ServerUrl.ForecastsUris[_forecastSize], response.ErrorMessage, response.ErrorException);

            jsonData = response.Content;
#endif
            
            var forecastsData = _jsonDeserializer.Deserialize(jsonData);
            return forecastsData;
        }
    }
}