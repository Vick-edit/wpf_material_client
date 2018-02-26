using System;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices;
using WPF_client.DomainServices.ConnectionProviders;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.Elements;
using WPF_client.Utilities.Formaters;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction.Builders.Forecasts
{
    public class MonthForecastPageBuilder : BaseForecastPageBuilder, IPageBuilder
    {
        protected override IForecastConnection GetForecastConnection()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer, ForecastSize.ByMonth);
            return forecastConnection;
        }

        protected override IFormater GetTimeFormater()
        {
            return new FormaterMonth();
        }
    }
}