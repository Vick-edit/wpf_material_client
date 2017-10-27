using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using WPF_client.Extensions;

namespace WPF_client.ViewModel
{
    public class PaletteSelectorViewModel
    {
        public IEnumerable<Swatch> Swatches { get; }

        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }


        public ICommand ToggleBaseCommand { get; } = new BaseCommandImplementation(a => ApplyBase((bool)a) );
        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        public ICommand ApplyPrimaryCommand { get; } = new BaseCommandImplementation(a => ApplyPrimary((Swatch)a) );
        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        public ICommand ApplyAccentCommand { get; } = new BaseCommandImplementation(a => ApplyAccent((Swatch)a) );
        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }
    }
}
