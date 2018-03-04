using System.Collections.Generic;

namespace WPF_client.Domain.DomainModels
{
    /// <summary> Блок информации за некий период </summary>
    public class ForecastBlock
    {
        /// <summary> Набор сведений о потреблении и прогнозов </summary>
        public List<Forecast> Forecasts { get; set; }

        /// <summary> Разница между прогнозом на последний и предыдущий день </summary>
        public double Consumption { get; set; }
    }
}