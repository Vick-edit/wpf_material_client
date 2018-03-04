using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_client.Converters
{
    /// <summary> Конвертер, используется, чтобы добавить к существующей кисти прозрачности </summary>
    public class OpacityBrushConverter : IValueConverter
    {
        /// <summary> Преобразовать кисть в кисть с прозрачностью </summary>
        /// <param name="value">Кисть <see cref="SolidColorBrush"/>, которая будет преобразована </param>
        /// <param name="targetType">Не используется, всегда возвращается <see cref="SolidColorBrush"/></param>
        /// <param name="parameter">Степень прозрачности кисти от 0d да 1d</param>
        /// <param name="culture">Не используется</param>
        /// <returns>Новая кисть с прозрачностью</returns>
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

        /// <summary> <see cref="NotImplementedException"/> </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}