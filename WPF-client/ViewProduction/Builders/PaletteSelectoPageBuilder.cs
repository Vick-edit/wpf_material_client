using System.Windows;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction.Builders
{
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