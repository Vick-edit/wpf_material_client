﻿using WPF_client.Domain;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.ServerDataProviders;
using WPF_client.Utilities.Formaters;
using WPF_client.ViewProduction.Interfaces;

namespace WPF_client.ViewProduction.Builders.Forecasts
{
    public class DayForecastPageBuilder : BaseForecastPageBuilder, IPageBuilder
    {
        protected override IGetSingleObjectRequest<ForecastBlock> GetForecastConnection()
        {
            var forecastDeserializer = new ForecastDeserializer();
            var forecastConnection = new GetForecastBlock(forecastDeserializer, ForecastSize.ByDay);
            return forecastConnection;
        }

        protected override IDateFormater GetTimeFormater()
        {
            return new FormaterDay();
        }
    }
}