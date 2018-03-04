using WPF_client.View;
using WPF_client.ViewModel;
using WPF_client.ViewProduction.Interfaces;

namespace WPF_client.ViewProduction.Builders
{
    /// <summary> Строитель для страницы с политрами </summary>
    public class PaletteSelectoPageBuilder : BasePageBuilder, IPageBuilder
    {
        public PaletteSelectoPageBuilder()
        {
            ViewModel = new PaletteSelectorViewModel();
            ViewElement = new PaletteSelector()
            {
                DataContext = ViewModel
            };
        }
    }
}