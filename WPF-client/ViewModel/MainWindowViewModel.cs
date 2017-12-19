using System;
using MaterialDesignThemes.Wpf;
using WPF_client.Elements;
using WPF_client.Utilities;
using WPF_client.ViewProduction;
using WPF_client.ViewProduction.Builders;

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
                builderDirector.GetPageContentItem("Прогноз на месяц", new MonthForecastPageBuilder()),
                builderDirector.GetPageContentItem("Прогноз на неделю", new WeekForecastPageBuilder()),
                builderDirector.GetPageContentItem("Прогноз на один день", new DayForecastPageBuilder()),

                builderDirector.GetPageContentItem("Цветовая тема", new PaletteSelectoPageBuilder()),
            };
        }

        public PageContentItem[] MainMenuItems { get; }

        public SnackbarMessageQueue RootMessageQueue { get; }
    }
}