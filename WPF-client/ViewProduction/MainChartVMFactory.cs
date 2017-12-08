using System;
using LiveCharts;
using LiveCharts.Defaults;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.ConnectionProviders;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction
{
    public class MainViewFactory : IViewFactory
    {
        /// <summary> График прогноза на месяц </summary>
        public MainChart GetMonthForecastView()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer);
            var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromDays(1));

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider)
            };
        }

        /// <summary> График прогноза на неделю </summary>
        public MainChart GetWeekForecast()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer);
            var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromDays(1));

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider)
            };
        }

        /// <summary> График прогноза на день </summary>
        public MainChart GetDayForecast()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer);
            var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromMinutes(15));

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider)
            };
        }


        /// <summary> Управление темами приложения </summary>
        public PaletteSelector GetPaletteSelectoView()
        {
            return new PaletteSelector
            {
                DataContext = new PaletteSelectorViewModel()
            };
        }
    }
}