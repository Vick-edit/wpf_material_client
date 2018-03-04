using WPF_client.View;

namespace WPF_client.ViewProduction.Interfaces
{
    /// <summary> Интерфейс фабрики вьюшек </summary>
    public interface IViewFactory
    {
        /// <summary> График прогноза на месяц </summary>
        ForecastsChart GetMonthForecastView();
        /// <summary> График прогноза на неделю </summary>
        ForecastsChart GetWeekForecast();
        /// <summary> График прогноза на день </summary>
        ForecastsChart GetDayForecast();

        /// <summary> Управление темами приложения </summary>
        PaletteSelector GetPaletteSelectoView();
    }
}