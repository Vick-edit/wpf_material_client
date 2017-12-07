using System;
using WPF_client.DomainServices.Exceptions;

namespace WPF_client.DomainServices.Events
{
    public delegate void ForecastConnectionError(object sender, ConnectionException updateError);
    public delegate void ForecastConnectionSuccess(object sender, string message);
}