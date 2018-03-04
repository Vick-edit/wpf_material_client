using RestSharp;
using WPF_client.Domain;
using WPF_client.Domain.DomainModels;
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
            var request = new RestRequest(ServerUrl.ForecastsUris[_forecastSize], Method.GET);
            request.AddUrlSegment("id", Session.Instance.ActiveForecastObjectId.ToString());
            var jsonData = _restClient.Execute(request).Content;
            var forecastsData = _jsonDeserializer.Deserialize(jsonData);
            return forecastsData;
        }
    }
}