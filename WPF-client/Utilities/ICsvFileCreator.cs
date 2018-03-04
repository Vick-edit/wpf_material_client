using System.Collections.Generic;

namespace WPF_client.Utilities
{
    /// <summary> Универсальный объект, создающий csv файлы со списком объектов </summary>
    public interface ICsvFileCreator
    {
        /// <summary> Сохранить в csv файл набор данных </summary>
        /// <typeparam name="T">Тип данных, которые будут сохранены</typeparam>
        /// <param name="fullFilePath">Полный путь до файла, в который будут сохранены данные</param>
        /// <param name="tableData">Список объектов, которые необходимо сохранить</param>
        /// <returns>Удалось сохранить или нет</returns>
        bool SaveToFile<T>(string fullFilePath, IList<T> tableData) where T : class ;
    }
}