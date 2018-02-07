using System;

namespace WPF_client.Domain.DomainModels
{
    public class Forecast
    {
        public double ForecastPower { get; set; }
        public DateTime ForecastTime { get; set; }
        public bool IsForecast { get; set; }

        public int DaySerialNumber { get; set; }
        public int WeekSerialNumber { get; set; }
        public int DayOfWeekNumber { get; set; }
        public bool IsItWeekend { get; set; }
    }
}