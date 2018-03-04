namespace WPF_client.ViewProduction
{
    //public class MainViewFactory : IViewFactory
    //{
    //    /// <summary> График прогноза на месяц </summary>
    //    public ForecastsChart GetMonthForecastView()
    //    {
    //        var forecastDeserializer = new ForecastDeserializer();
    //        var forecastConnection = new GetForecastBlock(forecastDeserializer);
    //        var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromDays(1));
    //        var csvFileCreator = new CsvFileCreator();

    //        var connectionErrorDialog = new ConnectionError();
    //        var dialogController = new DialogController(connectionErrorDialog);

    //        return new ForecastsChart
    //        {
    //            DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, csvFileCreator,  TimeSpan.FromDays(30))
    //        };
    //    }

    //    /// <summary> График прогноза на неделю </summary>
    //    public ForecastsChart GetWeekForecast()
    //    {
    //        var forecastDeserializer = new ForecastDeserializer();
    //        var forecastConnection = new GetForecastBlock(forecastDeserializer);
    //        var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromDays(1));
    //        var csvFileCreator = new CsvFileCreator();

    //        var connectionErrorDialog = new ConnectionError();
    //        var dialogController = new DialogController(connectionErrorDialog);

    //        return new ForecastsChart
    //        {
    //            DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, csvFileCreator, TimeSpan.FromDays(7))
    //        };
    //    }

    //    /// <summary> График прогноза на день </summary>
    //    public ForecastsChart GetDayForecast()
    //    {
    //        var forecastDeserializer = new ForecastDeserializer();
    //        var forecastConnection = new GetForecastBlock(forecastDeserializer);
    //        var forecastConnetionProvider = new ForecastProvider(forecastConnection, TimeSpan.FromMinutes(15));
    //        var csvFileCreator = new CsvFileCreator();

    //        var connectionErrorDialog = new ConnectionError();
    //        var dialogController = new DialogController(connectionErrorDialog);

    //        return new ForecastsChart
    //        {
    //            DataContext = new MainChartViewModel(forecastConnetionProvider, dialogController, csvFileCreator, TimeSpan.FromDays(1))
    //        };
    //    }


    //    /// <summary> Управление темами приложения </summary>
    //    public PaletteSelector GetPaletteSelectoView()
    //    {
    //        return new PaletteSelector
    //        {
    //            DataContext = new PaletteSelectorViewModel()
    //        };
    //    }
    //}
}