using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_client.Converters
{
    public class LeftMenuOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            return new Thickness(d, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LeftMenuHalfContentOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            return new Thickness(d / 2, 0, -d / 2, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LeftMenuZoomConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var d = values[0] as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) return 0.85;

            var s = values[1] as double? ?? 0;
            if (double.IsInfinity(s) || double.IsNaN(s)) return 0.85;

            var scale = (s - d) / s;
            if (double.IsInfinity(scale) || double.IsNaN(scale)) return 0.85;

            return scale;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}