using System.Collections.Generic;

namespace WPF_client.Domain.ServerConnection
{
    /// <summary> Интерфейс подключения к серверу для запроса данных </summary>
    public interface IGetCommand<T>
    {
        /// <summary> Получить список объектов с сервера </summary>
        /// <returns> Список объектов или пустой список </returns>
        IList<T> GetDataFromServer();
    }
}