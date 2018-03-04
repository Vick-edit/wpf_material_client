using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_client.Converters
{
    /// <summary> Конвертер, поерделяющий отображать заголовок <see cref="DateTooltip"/> или нет. </summary>
    public class SharedVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var visabilityNever = Visibility.Hidden;

            //Должно передоваться 2 параметра: данные тултипа и флаг отображать заголовок или нет.
            if (values?.Length != 2) return visabilityNever;

            //Проверим, что установлен флаг - отображать заголовок
            var showTitle = false;
            bool.TryParse(values[1]?.ToString(), out showTitle);
            if (!showTitle) return visabilityNever;

            //Проверим, что тултип получил данные подходящего формата 
            var tooltipData = values[0] as TooltipData;
            if (tooltipData == null) return visabilityNever;
            if (tooltipData.SharedValue == null && tooltipData.SelectionMode != TooltipSelectionMode.OnlySender) return visabilityNever;

            return Visibility.Visible;
        }

        /// <summary> <see cref="NotImplementedException"/> </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}