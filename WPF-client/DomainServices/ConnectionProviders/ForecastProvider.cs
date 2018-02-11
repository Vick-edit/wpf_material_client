using System;
using System.Collections.Generic;
using System.Threading;
using WPF_client.Domain.DomainModels;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.Events;
using WPF_client.DomainServices.Exceptions;
using WPF_client.Utilities;
using Timer = System.Timers.Timer;

namespace WPF_client.DomainServices.ConnectionProviders
{
    public class ForecastProvider : IForecastProvider
    {
        public event ForecastUpdate OnForecastUpdated;
        public event ForecastConnectionError OnConnectionLost;
        public event ForecastConnectionSuccess OnConnectionRestored;

        private readonly ReaderWriterLockSlim _forecastsLock = new ReaderWriterLockSlim();
        private ForecastBlock _forecastsBlock;
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

        private readonly IForecastConnection _forecastConnection;
        private readonly TimeSpan _retryTimout;
        private readonly TimeSpan _updatePeriod;

        private readonly Timer _refreshTimer;
        private readonly Timer _reconnectTimer;


        public ForecastProvider(IForecastConnection forecastConnection, TimeSpan updatePeriod)
        {
            _forecastConnection = forecastConnection;
            _retryTimout = TimeSpan.FromSeconds(15);
            _updatePeriod = updatePeriod;

            _refreshTimer = new Timer(_updatePeriod.TotalMilliseconds);
            _refreshTimer.Elapsed += (s, ea) => RefreshForecasts();

            _reconnectTimer = new Timer(_retryTimout.TotalMilliseconds);
            _reconnectTimer.Elapsed += (s, ea) => RefreshConnection();
        }


        public void StartWatchingForUpdates()
        {
            RefreshForecasts();
            _refreshTimer.Start();
        }

        public void StopWatchingForUpdates()
        {
            _refreshTimer.Stop();
            _reconnectTimer.Stop();
        }


        //Получить свежие данные с сервера
        private void RefreshForecasts(bool handleConnecError = true)
        {
            try
            {
                ForecastsBlock = _forecastConnection.GetForecasts();
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