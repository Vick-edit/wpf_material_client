using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using WPF_client.Extensions;
using WPF_client.Utilities;

namespace WPF_client.ViewModel
{
    public class PaletteSelectorViewModel : ViewModelBase
    {
        public IEnumerable<Swatch> Swatches { get; }
        public bool IsDarkThem { get; set; }

        private readonly PaletteHelper _paletteHelper;


        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
            IsDarkThem = ThemSettingsHandler.LoadSavedInstance().IsDarkThem;

            _paletteHelper = new PaletteHelper();
        }


        [MapCommand(nameof(ApplyBase))]
        public ICommand ToggleBaseCommand { get; private set; }
        private void ApplyBase(bool isDark)
        {
            _paletteHelper.SetLightDark(isDark);
            var savedSettings = ThemSettingsHandler.LoadSavedInstance();
            savedSettings.IsDarkThem = isDark;
            ThemSettingsHandler.LoadNewInstance(savedSettings);
        }

        [MapCommand(nameof(ApplyPrimary))]
        public ICommand ApplyPrimaryCommand { get; private set; }
        private void ApplyPrimary(Swatch swatch)
        {
            _paletteHelper.ReplacePrimaryColor(swatch);
            var savedSettings = ThemSettingsHandler.LoadSavedInstance();
            savedSettings.PrimaryThemColor = swatch.Name;
            ThemSettingsHandler.LoadNewInstance(savedSettings);
        }

        [MapCommand(nameof(ApplyAccent))]
        public ICommand ApplyAccentCommand { get; private set; }
        private void ApplyAccent(Swatch swatch)
        {
            _paletteHelper.ReplaceAccentColor(swatch);
            var savedSettings = ThemSettingsHandler.LoadSavedInstance();
            savedSettings.AccentThemColor = swatch.Name;
            ThemSettingsHandler.LoadNewInstance(savedSettings);
        }
    }
}
