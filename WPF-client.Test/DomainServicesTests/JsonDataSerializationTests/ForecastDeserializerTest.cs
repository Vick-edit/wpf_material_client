using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WPF_client.DomainServices.JsonDataSerialization;

namespace WPF_client.Test.DomainServicesTests.JsonDataSerializationTests
{
    [TestFixture]
    public class ForecastDeserializerTest
    {
        [Test]
        public void Deserialize_DeserializeCorrectJson_NotEmptyVlues()
        {
            //arrange
            var forecastDeserializer = new ForecastDeserializer();
            var jsonString = "{" +
                                "\"1388534400000\":{" +
                                                    "\"AP\":76160.0," +
                                                    "\"week\":1," +
                                                     "\"day\":1," +
                                                     "\"weekday\":2," +
                                                     "\"weekend\":0" +
                                                    "}," +
                                "\"1388536200000\":{" +
                                                    "\"AP\":76160.0," +
                                                    "\"week\":1," +
                                                     "\"day\":1," +
                                                     "\"weekday\":2," +
                                                     "\"weekend\":0" +
                                                    "}," +
                             "}";

            //act
            var forecastData = forecastDeserializer.Deserialize(jsonString);

            //assert
            Assert.IsNotNull(forecastData);
        }
    }
}