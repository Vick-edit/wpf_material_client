﻿using MaterialDesignThemes.Wpf;

namespace WPF_client.Utilities
{
    /// <summary> Класс, позволяющий сохранять тему оформления между сессиями пользователя </summary>
    public class ThemSettingsHandler
    {
        /// <summary> Извлечь сохраненную тему </summary>
        public static ThemSettings LoadSavedInstance()
        {
            var storedSettings = Properties.Settings.Default.ThemSettings;
            if (storedSettings != null) return storedSettings;

            storedSettings = new ThemSettingsHandler().GetDeffaultThemSettings();
            Properties.Settings.Default.ThemSettings = storedSettings;
            return storedSettings;
        }

        /// <summary> Сохранить тему, как дефолтную </summary>
        public static void LoadNewInstance(ThemSettings newThemSettings)
        {
            Properties.Settings.Default.ThemSettings = newThemSettings;
        }

        /// <summary> Задать в качестве темы оформления сохраненную тему </summary>
        public static void RestoreSettings()
        {
            ThemSettings storedSettings = LoadSavedInstance();
            new ThemSettingsHandler().SetUpSettings(storedSettings);
        }

        /// <summary> Сбросить все настройки по умолчанию </summary>
        public static void ResetSettings()
        {
            var themSettingsHandler = new ThemSettingsHandler();
            ThemSettings storedSettings = themSettingsHandler.GetDeffaultThemSettings();
            themSettingsHandler.SetUpSettings(storedSettings);
        }


        /// <summary> Приватный конструктор, используется только внутри статических методов этого класса </summary>
        private ThemSettingsHandler()
        {
        }

        /// <summary> Установить заданные настройки темы </summary>
        /// <param name="themSettings">Контейнер параметров темы</param>
        private void SetUpSettings(ThemSettings themSettings)
        {

            var palettHelper = new PaletteHelper();
            themSettings = themSettings ?? GetDeffaultThemSettings(palettHelper);

            var isDarkThem = themSettings.IsDarkThem;
            palettHelper.SetLightDark(isDarkThem);

            var primaryThemColor = string.IsNullOrEmpty(themSettings.PrimaryThemColor)
                ? GetDeffaultPrimaryThemColor(palettHelper)
                : themSettings.PrimaryThemColor;
            palettHelper.ReplacePrimaryColor(primaryThemColor);

            var accentThemColor = string.IsNullOrEmpty(themSettings.AccentThemColor)
                ? GetDeffaultAccentThemColor(palettHelper)
                : themSettings.AccentThemColor;
            palettHelper.ReplaceAccentColor(accentThemColor);
        }

        /// <summary> Получить настройки темы со значениями по умолчанию </summary>
        private ThemSettings GetDeffaultThemSettings(PaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? new PaletteHelper();

            var themSettings = new ThemSettings
            {
                IsDarkThem = GetDeffaultIsDarkThem(),
                PrimaryThemColor = GetDeffaultPrimaryThemColor(paletteHelper),
                AccentThemColor = GetDeffaultAccentThemColor(paletteHelper)
            };
            return themSettings;
        }


        /// <summary> Получить значение основного цвета по умолчанию </summary>
        private string GetDeffaultPrimaryThemColor(PaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? new PaletteHelper();
            return paletteHelper.QueryPalette().AccentSwatch.Name;
        }

        /// <summary> Получить значение дополнительного цвета по умолчанию </summary>
        private string GetDeffaultAccentThemColor(PaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? new PaletteHelper();
            return paletteHelper.QueryPalette().PrimarySwatch.Name;
        }

        /// <summary> Получить значение дополнительного цвета по умолчанию </summary>
        private bool GetDeffaultIsDarkThem()
        {
            return false;
        }
    }
}