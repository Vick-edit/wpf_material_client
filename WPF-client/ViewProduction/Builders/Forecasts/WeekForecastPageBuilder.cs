using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.Utilities.Formaters;

namespace WPF_client.ViewProduction.Builders.Forecasts
{
    public class WeekForecastPageBuilder : BaseForecastPageBuilder, IPageBuilder
    {
        protected override IForecastConnection GetForecastConnection()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer, ForecastSize.ByWeek);
            return forecastConnection;
        }

        protected override IFormater GetTimeFormater()
        {
            return new FormaterWeek();
        }
    }
}