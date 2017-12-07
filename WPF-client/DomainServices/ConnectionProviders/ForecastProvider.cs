using System.Collections.Generic;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.Events;

namespace WPF_client.DomainServices.ConnectionProviders
{
    public class ForecastProvider : IForecastProvider
    {
        public event ForecastUpdate OnForecastUpdated;
        public event ForecastConnectionError OnConnectionLost;
        public event ForecastConnectionSuccess OnConnectionRestored;

        public IList<Forecast> Forecasts { get; }


        public ForecastProvider()
        {
            
        }


        public void StartWatchingForUpdates()
        {
            throw new System.NotImplementedException();
        }

        public void StopWatchingForUpdates()
        {
            throw new System.NotImplementedException();
        }
    }
}