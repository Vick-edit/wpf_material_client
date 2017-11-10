using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using WPF_client.Extensions;

namespace WPF_client.ViewModel
{
    public class PaletteSelectorViewModel : ViewModelBase
    {
        public IEnumerable<Swatch> Swatches { get; }

        private readonly PaletteHelper _paletteHelper;


        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;

            _paletteHelper = new PaletteHelper();
        }


        [MapCommand(nameof(ApplyBase))]
        public ICommand ToggleBaseCommand { get; private set; }
        private void ApplyBase(bool isDark)
        {
            _paletteHelper.SetLightDark(isDark);
        }

        [MapCommand(nameof(ApplyPrimary))]
        public ICommand ApplyPrimaryCommand { get; private set; }
        private void ApplyPrimary(Swatch swatch)
        {
            _paletteHelper.ReplacePrimaryColor(swatch);
        }

        [MapCommand(nameof(ApplyAccent))]
        public ICommand ApplyAccentCommand { get; private set; }
        private void ApplyAccent(Swatch swatch)
        {
            _paletteHelper.ReplaceAccentColor(swatch);
        }
    }
}
