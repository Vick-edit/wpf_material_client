using NUnit.Framework;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.JsonDataSerialization;

namespace WPF_client.Test.Domain.ServerConnection
{
    [TestFixture]
    public class ForecastConnectionTest
    {
        [Test]
        public void ForecastConnection_ParseFile_NotEmptyData()
        {
            //arrange
            var parser = new ForecastDeserializer();
            var connection = new ForecastConnection(parser);

            //act
            var forecasts = connection.GetForecasts();

            //assert
            Assert.That(forecasts, Is.Not.Null.And.Not.Empty);
        }
    }
}