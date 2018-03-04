using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace WPF_client.Utilities
{
    /// <summary> Универсальный объект, создающий csv файлы со списком объектов </summary>
    public class CsvFileCreator : ICsvFileCreator
    {
        /// <summary> Сохранить в csv файл набор данных </summary>
        /// <typeparam name="T">Тип данных, которые будут сохранены</typeparam>
        /// <param name="fullFilePath">Полный путь до файла, в который будут сохранены данные</param>
        /// <param name="tableData">Список объектов, которые необходимо сохранить</param>
        /// <returns>Удалось сохранить или нет</returns>
        public bool SaveToFile<T>(string fullFilePath, IList<T> tableData) where T : class
        {
            using (var fileStream = File.Open(fullFilePath, FileMode.Create))
            using (var csvString = new StreamWriter(fileStream))
            using (var csv = new CsvWriter(csvString))
            {
                csv.Configuration.Delimiter = ";";
                csv.WriteHeader(typeof (T));
                csv.NextRecord();
                csv.WriteRecords(tableData);
            }
            return File.Exists(fullFilePath);
        }
    }
}