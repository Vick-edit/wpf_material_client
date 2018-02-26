using System;
using System.Collections.Generic;
using System.IO;
using WPF_client.Utilities;

namespace WPF_client.Domain.ServerConnection
{
    public static class ServerUrl
    {
        public static readonly string ServerName = GetServerName();

        public static readonly string ForecastsObjectUri = "localities";

        public static Dictionary<ForecastSize, string> ForecastsUris = new Dictionary<ForecastSize, string>()
        {
            {ForecastSize.ByDay, "predict/day/{id}" },
            {ForecastSize.ByWeek, "predict/week/{id}" },
            {ForecastSize.ByMonth, "predict/month/{id}" },
            {ForecastSize.ByYear, "predict/year/{id}" }
        };

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