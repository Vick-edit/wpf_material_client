using System;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.ConnectionProviders;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.Elements;
using WPF_client.Utilities.WPF.ElementControllers;
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

            var connectionErrorDialog = new ConnectionError();
            var dialogController = new DialogController(connectionErrorDialog);

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, TimeSpan.FromDays(30))
            };
        }

        /// <summary> График прогноза на неделю </summary>
        public MainChart GetWeekForecast()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer);
            var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromDays(1));

            var connectionErrorDialog = new ConnectionError();
            var dialogController = new DialogController(connectionErrorDialog);

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, TimeSpan.FromDays(7))
            };
        }

        /// <summary> График прогноза на день </summary>
        public MainChart GetDayForecast()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new ForecastConnection(forecastDeserializer);
            var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromMinutes(15));

            var connectionErrorDialog = new ConnectionError();
            var dialogController = new DialogController(connectionErrorDialog);

            return new MainChart
            {
                DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, TimeSpan.FromDays(1))
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