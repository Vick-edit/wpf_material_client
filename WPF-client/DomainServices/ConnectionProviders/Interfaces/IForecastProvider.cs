using System;
using System.Collections.Generic;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.Events;

namespace WPF_client.DomainServices.ConnectionProviders
{
    public interface IForecastProvider : IDisposable
    {
        event ForecastUpdate OnForecastUpdated;
        event ForecastConnectionError OnConnectionLost;
        event ForecastConnectionSuccess OnConnectionRestored;

        IList<Forecast> Forecasts { get; }

        void StartWatchingForUpdates();
        void StopWatchingForUpdates();
    }
}