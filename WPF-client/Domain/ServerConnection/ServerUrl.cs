using System;
using System.IO;
using WPF_client.Utilities;

namespace WPF_client.Domain.ServerConnection
{
    public static class ServerUrl
    {
        public static readonly string ServerName = GetServerName();

        public static readonly string ForecastsObject = "localities";

        public static readonly string ForecastsData = "predict/{id}";

        private static string GetServerName()
        {
            var serverName = "localhost:80";

            try
            {
                serverName = File.ReadAllText("connect.config").Trim();
            }
            catch (Exception e)
            {
                ExceptionLogger.Log(e);
            }

            return serverName;
        }
    }
}