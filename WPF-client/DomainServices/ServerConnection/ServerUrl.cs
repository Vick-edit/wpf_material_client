using System;
using System.Collections.Generic;
using System.IO;
using WPF_client.Domain;
using WPF_client.Utilities;

namespace WPF_client.DomainServices.ServerConnection
{
    /// <summary> Список URL сервера </summary>
    public static class ServerUrl
    {
        /// <summary> Root URL </summary>
        public static readonly string ServerName = GetServerName();

        /// <summary> URL объектов исследования </summary>
        public static readonly string ForecastsObjectUrl = "localities";

        /// <summary> URL данных о поргнозе в зависимости от периода прогнозирования </summary>
        public static Dictionary<ForecastSize, string> ForecastsUris = new Dictionary<ForecastSize, string>()
        {
            {ForecastSize.ByDay, "predict/day/{id}" },
            {ForecastSize.ByWeek, "predict/week/{id}" },
            {ForecastSize.ByMonth, "predict/month/{id}" },
            {ForecastSize.ByYear, "predict/year/{id}" }
        };

        //Извлекаем имя сервера из конфиг файла
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