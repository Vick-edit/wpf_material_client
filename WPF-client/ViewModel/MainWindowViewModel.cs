using WPF_client.Elements;
using WPF_client.ViewProduction;

namespace WPF_client.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            var viewFactory = new MainViewFactory();

            MainMenuItems = new PageContentItem[]
            {
                new PageContentItem("Прогноз на месяц", viewFactory.GetMonthForecastView()),
                new PageContentItem("Прогноз на неделю", viewFactory.GetWeekForecast()),
                new PageContentItem("Прогноз на один день", viewFactory.GetDayForecast()),
                new PageContentItem("Цветовая тема", viewFactory.GetPaletteSelectoView()),
            };
        }

        public PageContentItem[] MainMenuItems { get; }
    }
}