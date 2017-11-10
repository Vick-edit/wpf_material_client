using System;
using MaterialDesignThemes.Wpf;

namespace WPF_client.Utilities
{
    [Serializable]
    public class ThemSettings
    {
        public bool IsDarkThem { get; set; }
        public string PrimaryThemColor { get; set; }
        public string AccentThemColor { get; set; }
    }
}