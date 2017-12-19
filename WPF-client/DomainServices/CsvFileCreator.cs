using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace WPF_client.DomainServices
{
    public class CsvFileCreator : ICsvFileCreator
    {
        public bool SaveToFile<T>(string fullFilePath, IList<T> forecasts) where T : class
        {
            using (var fileStream = File.Open(fullFilePath, FileMode.Create))
            using (var csvString = new StreamWriter(fileStream))
            using (var csv = new CsvWriter(csvString))
            {
                csv.Configuration.Delimiter = ";";
                csv.WriteHeader(typeof (T));
                csv.NextRecord();
                csv.WriteRecords(forecasts);
            }
            return File.Exists(fullFilePath);
        }
    }
}