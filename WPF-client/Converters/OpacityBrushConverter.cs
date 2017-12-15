using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_client.Converters
{
    public class OpacityBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bindedBrush = value as SolidColorBrush;
            if (bindedBrush == null)
                return value;

            var opacity = 1d;
            if (parameter != null)
            {
                var parametrStr = parameter.ToString().Replace(',', '.');
                var numberStyle = NumberStyles.AllowThousands | NumberStyles.Float;
                var formatProvider = CultureInfo.InvariantCulture;
                var converted = double.TryParse(parametrStr, numberStyle, formatProvider, out opacity);
                if (!converted)
                    opacity = 1d;

                opacity = Math.Min(1d, opacity);
                opacity = Math.Max(0d, opacity);
            }

            var newBrush = new SolidColorBrush(bindedBrush.Color)
            {
                Opacity = opacity,
            };
            return newBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}