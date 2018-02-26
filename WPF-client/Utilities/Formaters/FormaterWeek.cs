using System;
using System.Globalization;

namespace WPF_client.Utilities.Formaters
{
    public class FormaterWeek : IFormater
    {
        public string DateFormatter(double timeInSeconds)
        {
            var date = new DateTime((long) timeInSeconds);
            var weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return $"{date:yyyy}г. неделя №{weekNum}";
        }

        public string SimpleDateFormatter(double timeInSeconds)
        {
            var date = new DateTime((long)timeInSeconds);
            var weekNum = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return $"{date:yyyy}, н№{weekNum}";
        }
    }
}