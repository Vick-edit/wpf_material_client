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

            var isBeginOfDay = tooltipDate.Hour == 0
                          && tooltipDate.Minute == 0
                          && tooltipDate.Second == 0
                          && tooltipDate.Millisecond == 0;
            var dateFormat = isBeginOfDay ? "dd MMM yyyy" : "dd MMM yyyy - HH:MM";

            return tooltipDate.ToString(dateFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}