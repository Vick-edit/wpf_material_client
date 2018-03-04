using NUnit.Framework;
using WPF_client.DomainServices.JsonDataSerialization;

namespace WPF_client.Test.DomainServicesTests.JsonDataSerialization
{
    [TestFixture]
    public class ForecastDeserializerTest
    {
        [Test]
        public void Deserialize_DeserializeCorrectJson_NotEmptyVlues()
        {
            //arrange
            var forecastDeserializer = new ForecastDeserializer();
            var jsonString = "{\"points\": [" +
                                                "{" +
                                                    "\"ap\": 93089387.52," +
                                                    "\"date\": \"01.07.2014\"," +
                                                    "\"is_predict\": true" +
                                                "}," +
                                                "{" +
                                                    "\"ap\": 93089387.25," +
                                                    "\"date\": \"01.08.2014\"," +
                                                    "\"is_predict\": false" +
                                                "}" +
                                           "]," +
                                "\"consumption\": \"2329.00\"" +
                             "}";

            //act
            var forecastData = forecastDeserializer.Deserialize(jsonString);

            //assert
            Assert.IsNotNull(forecastData);
        }
    }
}