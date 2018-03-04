using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_client.Converters
{
    /// <summary>
    /// WPF конвертер, преобразует данные точки графика <see cref="TooltipData"/>, где по оси Х откладываются даты 
    /// в текстовую строку для <see cref="DateTooltip"/> и прочих форматеров.
    /// </summary>
    public class DatePointToStringCoverter : IValueConverter
    {
        /// <summary>
        /// Преобразовать данные из точки графика <see cref="TooltipData"/> в строку
        /// </summary>
        /// <param name="value">Данные <see cref="TooltipData"/></param>
        /// <param name="targetType">Не используется, преобразуется всегда в <see cref="string"/></param>
        /// <param name="parameter">Не используется</param>
        /// <param name="culture">Не используется</param>
        /// <returns>Форматированная дата в строковм представлении</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime tooltipDate;
            double dateValue;

            var tooltipData = value as TooltipData;
            if (tooltipData == null) return null;

            if (tooltipData.SelectionMode == TooltipSelectionMode.OnlySender)
            {
                var chartPoint = tooltipData.Points?.FirstOrDefault()?.ChartPoint;
                if (chartPoint == null) return string.Empty;

                dateValue = chartPoint.X;
                tooltipDate = new DateTime((long) chartPoint.X);
            }
            else
            {
                if (tooltipData.SharedValue == null) return string.Empty;

                dateValue = tooltipData.SharedValue.Value;
                tooltipDate = new DateTime((long)tooltipData.SharedValue);
            }

            if (tooltipDate == DateTime.MinValue || tooltipDate == DateTime.MaxValue) return string.Empty;

            //var isBeginOfDay = tooltipDate.Hour == 0
            //  && tooltipDate.Minute == 0
            //  && tooltipDate.Second == 0
            //  && tooltipDate.Millisecond == 0;
            //var dateFormat = isBeginOfDay ? "dd MMM yyyy" : "dd MMM yyyy - HH:MM";

            return tooltipData.XFormatter(dateValue);
        }

        /// <summary> <see cref="NotImplementedException"/> </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}