using System.Collections.Generic;
using RestSharp;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;

namespace WPF_client.Domain.ServerConnection
{
    public class GetForecastObjects: IGetCommand<ForecastJsonObject>
    {
        private readonly IJsonDeserializer<ForecastJsonObject> _jsonDeserializer;
        private readonly RestClient _restClient;

        public GetForecastObjects(IJsonDeserializer<ForecastJsonObject> jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
            _restClient = new RestClient(ServerUrl.ServerName);
        }

        public IList<ForecastJsonObject> GetDataFromServer()
        {
            var request = new RestRequest(ServerUrl.ForecastsObject, Method.GET);
            var response = _restClient.Execute(request);
            var forecastsObjects = _jsonDeserializer.Deserialize(response.Content);
            return forecastsObjects;
        }
    }
}