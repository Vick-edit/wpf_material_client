using System;
using MaterialDesignThemes.Wpf;
using WPF_client.Elements;
using WPF_client.Utilities;
using WPF_client.ViewProduction;
using WPF_client.ViewProduction.Builders;
using WPF_client.ViewProduction.Builders.Forecasts;

namespace WPF_client.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            RootMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(10));
            Session.Instance.SnackbarMessageQueue = RootMessageQueue;

            var builderDirector = new PageBuilderDirector();
            MainMenuItems = new PageContentItem[]
            {
                builderDirector.GetPageContentItem("Объект прогнозирования", new ForecastObjectPageBuilder()),

                builderDirector.GetPageContentItem("Прогноз по дням", new DayForecastPageBuilder()),
                builderDirector.GetPageContentItem("Прогноз по неделям", new WeekForecastPageBuilder()),
                builderDirector.GetPageContentItem("Прогноз по месяцам", new MonthForecastPageBuilder()),
                builderDirector.GetPageContentItem("Прогноз по годам", new YerForecastPageBuilder()),

                builderDirector.GetPageContentItem("Цветовая тема", new PaletteSelectoPageBuilder()),
            };
        }

        public PageContentItem[] MainMenuItems { get; }

        public SnackbarMessageQueue RootMessageQueue { get; }
    }
}