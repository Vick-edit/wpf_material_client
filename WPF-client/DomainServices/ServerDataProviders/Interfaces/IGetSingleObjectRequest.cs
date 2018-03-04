namespace WPF_client.DomainServices.ServerDataProviders
{
    /// <summary> Интерфейс подключения к серверу для запроса единичного объекта данных  </summary>
    public interface IGetSingleObjectRequest<T> where T : class 
    {
        /// <summary> Получить объект от сервера </summary>
        /// <returns> Запрашиваемый объект или null </returns>
        T GetDataFromServer();
    }
}