using WPF_client.Elements;
using WPF_client.ViewProduction;
using WPF_client.ViewProduction.Builders;

namespace WPF_client.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
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
    }
}