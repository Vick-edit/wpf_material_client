using System;

namespace WPF_client.DomainServices.Exceptions
{
    /// <summary> Ошибка соединения с сервером </summary>
    public class ConnectionException : Exception
    {
        public string UrlError { get; set; }

        public ConnectionException(string urlError) : base()
        {
            UrlError = urlError;
        }

        public ConnectionException(string urlError, string message) : base(message)
        {
            UrlError = urlError;
        }

        public ConnectionException(string urlError, string message, Exception innerException) : base(message, innerException)
        {
            UrlError = urlError;
        }
    }
}