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
            DateTime tooltipDate;

            var tooltipData = value as TooltipData;
            if (tooltipData == null) return null;

            if (tooltipData.SelectionMode == TooltipSelectionMode.OnlySender)
            {
                var chartPoint = tooltipData.Points?.FirstOrDefault()?.ChartPoint;
                if (chartPoint == null) return string.Empty;

                tooltipDate = new DateTime((long) chartPoint.X);
            }
            else
            {
                if (tooltipData.SharedValue == null) return string.Empty;

                tooltipDate = new DateTime((long)tooltipData.SharedValue);
            }

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