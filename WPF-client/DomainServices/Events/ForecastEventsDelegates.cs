using System.Collections.Generic;
using WPF_client.Domain.DomainModels;

namespace WPF_client.DomainServices.Events
{
    public delegate void ForecastUpdate(object sender, IList<Forecast> forecasts);
}