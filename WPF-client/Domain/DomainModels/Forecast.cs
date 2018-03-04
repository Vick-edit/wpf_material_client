using System;

namespace WPF_client.Domain.DomainModels
{
    /// <summary> Объект, содержащий информацию о прогнозе/реальном потреблении на какой-то момент времени </summary>
    public class Forecast
    {
        public double ForecastPower { get; set; }
        public DateTime ForecastTime { get; set; }
        public bool IsForecast { get; set; }
    }
}