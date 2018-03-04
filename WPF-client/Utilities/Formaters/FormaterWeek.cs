using System;
using System.Globalization;

namespace WPF_client.Utilities.Formaters
{
    /// <summary> Форматер времени, для возвращения сведений о неделе с начала года в дате </summary>
    public class FormaterWeek : IDateFormater
    {
        /// <summary> Базовый форматер, переводит дату-время из числа тактов в строку сведений о неделе с начала года в дате </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        public string DateFormatter(double timeInSeconds)
        {
            var date = new DateTime((long) timeInSeconds);
            var weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return $"{date:yyyy}г. неделя №{weekNum}";
        }

        /// <summary> Упрощенный форматер, переводит дату-время из числа тактов в строку сведений о неделе с начала года в дате </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        public string SimpleDateFormatter(double timeInSeconds)
        {
            var date = new DateTime((long)timeInSeconds);
            var weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return $"{date:yyyy}, н№{weekNum}";
        }
    }
}