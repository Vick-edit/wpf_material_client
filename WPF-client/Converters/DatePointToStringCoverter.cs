using System;
using System.Globalization;
using System.Windows.Data;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_client.Converters
{
    public class DatePointToStringCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tooltipData = value as TooltipData;

            if (tooltipData == null) return null;

            if (tooltipData.SelectionMode == TooltipSelectionMode.OnlySender) return string.Empty;
            if (tooltipData.SharedValue == null) return string.Empty;

            var tooltipDate = new DateTime((long)tooltipData.SharedValue);
            if (tooltipDate == DateTime.MinValue || tooltipDate == DateTime.MaxValue) return string.Empty;

            return tooltipDate.ToString("dd MMM yyyy - HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}