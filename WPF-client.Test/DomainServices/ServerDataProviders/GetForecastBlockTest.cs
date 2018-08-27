using NUnit.Framework;
using WPF_client.Domain;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.ServerConnection;
using WPF_client.DomainServices.ServerDataProviders;
using WPF_client.Utilities;

namespace WPF_client.Test.DomainServices.ServerDataProviders
{
    [TestFixture]
    public class GetForecastBlockTest
    {
        [Test]
        public void ForecastConnection_ParseFile_NotEmptyData()
        {
            //arrange
            Session.Instance.ActiveForecastObjectId = 1;
            var parser = new ForecastDeserializer();
            var connection = new GetForecastBlock(parser, ForecastSize.ByMonth, ServerUrl.ServerName);

            //act
            var forecastBlock = connection.GetDataFromServer();

            //assert
            Assert.That(forecastBlock, Is.Not.Null);
            Assert.That(forecastBlock.Forecasts, Is.Not.Null.And.Not.Empty);
        }
    }
}