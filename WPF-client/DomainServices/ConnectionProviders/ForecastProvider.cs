using System;
using System.Collections.Generic;
using WPF_client.Domain.DomainModels;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.Events;
using WPF_client.DomainServices.Exceptions;
using WPF_client.Utilities;

namespace WPF_client.DomainServices.ConnectionProviders
{
    public class ForecastProvider : IForecastProvider
    {
        public event ForecastUpdate OnForecastUpdated;
        public event ForecastConnectionError OnConnectionLost;
        public event ForecastConnectionSuccess OnConnectionRestored;

        public IList<Forecast> Forecasts { get; private set; }

        private readonly IForecastConnection _forecastConnection;
        private readonly DateTime _updatePeriod;


        public ForecastProvider(IForecastConnection forecastConnection, DateTime updatePeriod)
        {
            _forecastConnection = forecastConnection;
            _updatePeriod = updatePeriod;
        }


        public void StartWatchingForUpdates()
        {
#if DEBUG
            RefreshForecasts();
#else
            throw new System.NotImplementedException();
#endif
        }

        public void StopWatchingForUpdates()
        {
            throw new System.NotImplementedException();
        }


        //Получить свежие данные с сервера
        private void RefreshForecasts()
        {
            try
            {
                Forecasts = _forecastConnection.GetForecasts();
                OnForecastUpdated?.Invoke(this, Forecasts);
            }
            catch (ConnectionException e)
            {
                ExceptionLogger.Log(e);
                OnConnectionLost?.Invoke(this, e);
            }
        }
    }
}