using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using WPF_client.Domain.DomainModels;
using WPF_client.Utilities;

namespace WPF_client.Test.Utilities
{
    [TestFixture]
    public class CsvFileCreatorTests
    {
        [Test]
        public void CorrectNotEmptyData_SaveToFile_CorrectFile()
        {
            //arrange
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.csv");
            var forecastsList = new List<Forecast>();

            var baseTime = DateTime.Now;
            for (var i = 1; i <= 7; i++)
            {
                forecastsList.Add(
                    new Forecast()
                    {
                        ForecastPower = i*1000.1234,
                        ForecastTime = baseTime.AddDays(i),
                    });
            };

            var csvFileCreator = new CsvFileCreator();


            //act
            var result = csvFileCreator.SaveToFile(fileName, forecastsList);


            //assert
            Assert.That(result, Is.True);
        }
    }
}