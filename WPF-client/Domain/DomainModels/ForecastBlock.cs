using System.Collections.Generic;

namespace WPF_client.Domain.DomainModels
{
    public class ForecastBlock
    {
        public List<Forecast> Forecasts { get; set; }
        public double Consumption { get; set; }
    }
}