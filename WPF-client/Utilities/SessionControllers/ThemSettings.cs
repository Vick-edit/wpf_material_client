using System;

namespace WPF_client.Utilities
{
    /// <summary> Сериализуемые сведения о настройках темы, сохраняющиеся между сессиями </summary>
    [Serializable]
    public class ThemSettings
    {
        public bool IsDarkThem { get; set; }
        public string PrimaryThemColor { get; set; }
        public string AccentThemColor { get; set; }
    }
}