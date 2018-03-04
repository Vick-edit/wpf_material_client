using System;
using System.Threading;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.Exceptions;
using WPF_client.DomainServices.ServerDataProviders;
using WPF_client.Utilities;
using WPF_client.WPFServices.Events;
using Timer = System.Timers.Timer;

namespace WPF_client.WPFServices.DataProviderWrappers
{
    /// <summary>
    ///     Сервис, позволяющий получать данные о прогнозе с сервера с некоторой переодичностью 
    ///     и отслеживать состояние сервиса
    /// </summary>
    public class ForecastProvider : IForecastProvider
    {
        #region IForecastProvider data
        /// <summary> Событие, вызывающееся, когда были получены данные от сервера </summary>
        public event ForecastUpdate OnForecastUpdated;
        /// <summary> Событие, вызывающееся, если произошел сбой в соединении, при попытке получить данные от сервера </summary>
        public event ForecastConnectionError OnConnectionLost;
        /// <summary> Событие, вызывающееся, если после потери соединения оно было восстановленно </summary>
        public event ForecastConnectionSuccess OnConnectionRestored;

        private readonly ReaderWriterLockSlim _forecastsLock = new ReaderWriterLockSlim();
        private ForecastBlock _forecastsBlock;
        /// <summary> Последние полученные от сервера данные о прогнозе </summary>
        public ForecastBlock ForecastsBlock
        {
            get
            {
                _forecastsLock.EnterReadLock();
                try
                {
                    return _forecastsBlock;
                }
                finally
                {
                    _forecastsLock.ExitReadLock();
                }
            }
            private set
            {
                _forecastsLock.EnterWriteLock();
                try
                {
                    _forecastsBlock = value;
                }
                finally
                {
                    _forecastsLock.ExitWriteLock();
                }
            }
        } 
        #endregion

        //Служебные данные
        private readonly IGetSingleObjectRequest<ForecastBlock>     _forecastConnection;    //Доступ к данным от сервера
        private readonly TimeSpan                                   _retryTimout;           //Таймаут между повторными попутыками соединения при потере связи
        private readonly TimeSpan                                   _updatePeriod;          //Врея обновления данные

        private readonly Timer                                      _refreshTimer;          //Таймер, позволяющий отслеживать время между обновлениями данных
        private readonly Timer                                      _reconnectTimer;        //Таймер, позволяющий отслеживать время между поторными попытками соединения с сервером


        public ForecastProvider(IGetSingleObjectRequest<ForecastBlock> forecastConnection, TimeSpan updatePeriod)
        {
            _forecastConnection = forecastConnection;
            _retryTimout = TimeSpan.FromSeconds(15);
            _updatePeriod = updatePeriod;

            _refreshTimer = new Timer(_updatePeriod.TotalMilliseconds);
            _refreshTimer.Elapsed += (s, ea) => RefreshForecasts();

            _reconnectTimer = new Timer(_retryTimout.TotalMilliseconds);
            _reconnectTimer.Elapsed += (s, ea) => RefreshConnection();
        }


        #region IForecastProvider methods
        /// <summary> Начать переодическое получение сведений о прогнозе от сервера </summary>
        public void StartWatchingForUpdates()
        {
            RefreshForecasts();
            _refreshTimer.Start();
        }

        /// <summary> Прекратить преодическое получение сведений от сервера </summary>
        public void StopWatchingForUpdates()
        {
            _refreshTimer.Stop();
            _reconnectTimer.Stop();
        } 
        #endregion


        //Получить свежие данные с сервера
        private void RefreshForecasts(bool handleConnecError = true)
        {
            try
            {
                ForecastsBlock = _forecastConnection.GetDataFromServer();
                OnForecastUpdated?.Invoke(this, ForecastsBlock);
            }
            catch (ConnectionException e)
            {
                if (handleConnecError)
                {
                    ExceptionLogger.Log(e);
                    OnConnectionLost?.Invoke(this, e);
                    _reconnectTimer.Start();
                }
                else
                {
                    throw;
                }
            }
        }

        //Востановить соединение с сервером
        private void RefreshConnection()
        {
            try
            {
                RefreshForecasts(false);
                _reconnectTimer.Stop();
                OnConnectionRestored?.Invoke(this, "Подключение востановленно");
            }
            catch (ConnectionException e)
            {
                ExceptionLogger.Log(e);
            }
        }


        public void Dispose()
        {
            _refreshTimer?.Dispose();
            _reconnectTimer?.Dispose();
        }
    }
}