using System.Collections.Generic;
using WPF_client.Domain.DomainModels;

namespace WPF_client.Domain.ServerConnection
{
    /// <summary> Интерфейс подключения к серверу для получения прогнозов </summary>
    public interface IForecastConnection
    {
        /// <summary> Метод, который получает свежую порцию данных с сервера </summary>
        ForecastBlock GetForecasts();
    }
}