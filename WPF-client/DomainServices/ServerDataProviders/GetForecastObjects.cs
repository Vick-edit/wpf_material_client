using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.Exceptions;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;
using WPF_client.DomainServices.ServerConnection;

namespace WPF_client.DomainServices.ServerDataProviders
{
    /// <summary> GET метод, для получнеия списка объектов прогнозирвоания </summary>
    public class GetForecastObjects: IGetListRequest<ForecastObject>
    {
        private readonly IJsonDeserializer<ForecastObject> _jsonDeserializer;
        private readonly RestClient _restClient;


        public GetForecastObjects(IJsonDeserializer<ForecastObject> jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
            _restClient = new RestClient(ServerUrl.ServerName);
        }


        /// <summary> Получить список возможных объектов прогнозирвоания </summary>
        /// <returns>Список объектов по которым можно узнать прогноз потребления электроэнергии</returns>
        public IList<ForecastObject> GetDataFromServer()
        {
            string jsonData;
#if DEBUG
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var rootFolder = Directory.GetParent(exePath).Parent.Parent.Parent.FullName;
            var jsonFile = Path.Combine(rootFolder, "WPF-client.Test", "TestData", "predict.json");

            jsonData = File.ReadAllText(jsonFile);
#else
            var request = new RestRequest(ServerUrl.ForecastsObjectUrl, Method.GET);
            var response = _restClient.Execute(request);
            jsonData = response.Content;
#endif
            var forecastsObjects = _jsonDeserializer.Deserialize(jsonData);
            return forecastsObjects;
        }
    }
}