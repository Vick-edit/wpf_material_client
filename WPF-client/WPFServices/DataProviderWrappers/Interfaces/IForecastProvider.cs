using System;
using WPF_client.Domain.DomainModels;
using WPF_client.WPFServices.Events;

namespace WPF_client.WPFServices.DataProviderWrappers
{
    /// <summary>
    ///     Интерфейс сервиса, позволяющий получать данные о прогнозе с сервера с некоторой переодичностью 
    ///     и отслеживать состояние сервиса
    /// </summary>
    public interface IForecastProvider : IDisposable
    {
        /// <summary> Событие, вызывающееся, когда были получены данные от сервера </summary>
        event ForecastUpdate OnForecastUpdated;
        /// <summary> Событие, вызывающееся, если произошел сбой в соединении, при попытке получить данные от сервера </summary>
        event ForecastConnectionError OnConnectionLost;
        /// <summary> Событие, вызывающееся, если после потери соединения оно было восстановленно </summary>
        event ForecastConnectionSuccess OnConnectionRestored;


        /// <summary> Последние полученные от сервера данные о прогнозе </summary>
        ForecastBlock ForecastsBlock { get; }


        /// <summary> Начать переодическое получение сведений о прогнозе от сервера </summary>
        void StartWatchingForUpdates();
        /// <summary> Прекратить преодическое получение сведений от сервера </summary>
        void StopWatchingForUpdates();
    }
}