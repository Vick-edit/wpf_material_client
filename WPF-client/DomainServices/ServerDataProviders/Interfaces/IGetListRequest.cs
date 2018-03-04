using System.Collections.Generic;

namespace WPF_client.DomainServices.ServerDataProviders
{
    /// <summary> Интерфейс подключения к серверу для запроса списка данных </summary>
    public interface IGetListRequest<T>
    {
        /// <summary> Получить список объектов с сервера </summary>
        /// <returns> Список объектов или пустой список </returns>
        IList<T> GetDataFromServer();
    }
}