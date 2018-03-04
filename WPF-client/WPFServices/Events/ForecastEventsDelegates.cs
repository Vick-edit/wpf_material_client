using WPF_client.Domain.DomainModels;

namespace WPF_client.WPFServices.Events
{
    public delegate void ForecastUpdate(object sender, ForecastBlock forecasts);
}