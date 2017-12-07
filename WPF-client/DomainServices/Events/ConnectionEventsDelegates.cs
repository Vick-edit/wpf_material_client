using System;

namespace WPF_client.DomainServices.Events
{
    public delegate void ForecastConnectionError(object sender, Exception updateError);
    public delegate void ForecastConnectionSuccess(object sender, string message);
}