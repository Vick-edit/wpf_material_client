using System;
using System.Globalization;
using System.Linq;
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

            if (tooltipData.SelectionMode == TooltipSelectionMode.OnlySender)
            {
                var chartPoint = tooltipData.Points?.FirstOrDefault()?.ChartPoint;
                if (chartPoint == null) return string.Empty;

                var chartPointDate = new DateTime((long) chartPoint.X);
                if (chartPointDate == DateTime.MinValue || chartPointDate == DateTime.MaxValue) return string.Empty;

                return chartPointDate.ToString("dd MMM yyyy - HH:mm");
            }
            else
            {
                if (tooltipData.SharedValue == null) return string.Empty;

                var tooltipDate = new DateTime((long)tooltipData.SharedValue);
                if (tooltipDate == DateTime.MinValue || tooltipDate == DateTime.MaxValue) return string.Empty;

                return tooltipDate.ToString("dd MMM yyyy - HH:mm");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}