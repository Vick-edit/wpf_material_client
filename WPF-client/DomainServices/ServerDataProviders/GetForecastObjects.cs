using System.Collections.Generic;
using RestSharp;
using WPF_client.Domain.DomainModels;
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
            var request = new RestRequest(ServerUrl.ForecastsObjectUrl, Method.GET);
            var response = _restClient.Execute(request);
            var forecastsObjects = _jsonDeserializer.Deserialize(response.Content);
            return forecastsObjects;
        }
    }
}