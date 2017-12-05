using WPF_client.View;

namespace WPF_client.ViewProduction
{
    /// <summary> Интерфейс фабрики вьюшек </summary>
    public interface IViewFactory
    {
        /// <summary> График прогноза на месяц </summary>
        MainChart GetMonthForecastView();
        /// <summary> График прогноза на неделю </summary>
        MainChart GetWeekForecast();
        /// <summary> График прогноза на день </summary>
        MainChart GetDayForecast();

        /// <summary> Управление темами приложения </summary>
        PaletteSelector GetPaletteSelectoView();
    }
}