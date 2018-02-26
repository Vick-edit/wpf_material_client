﻿using System;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices;
using WPF_client.DomainServices.ConnectionProviders;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.Elements;
using WPF_client.Utilities.Formaters;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction.Builders.Forecasts
{
    public abstract class BaseForecastPageBuilder : BasePageBuilder, IPageBuilder
    {
        private IForecastProvider _forecastProvider;
        private ICsvFileCreator _csvFileCreator;

        public override void SetupBuisnesLogic()
        {
            base.SetupBuisnesLogic();

            var forecastConnection = GetForecastConnection();
            _forecastProvider = new ForecastProvider(forecastConnection, TimeSpan.FromMinutes(5));
            _csvFileCreator = new CsvFileCreator();
        }

        public override void SetupViewModel()
        {
            base.SetupViewModel();

            if (_forecastProvider == null || _csvFileCreator == null)
                throw new NullReferenceException("Не задана бизнеслогика страницы");

            var dialogElement = new ConnectionError();
            DialogController = new DialogController(dialogElement);

            var formater = GetTimeFormater();
            var forecastPeriod = TimeSpan.FromDays(1);

            ViewModel = new MainChartViewModel(_forecastProvider, DialogController, _csvFileCreator, formater, forecastPeriod);
        }

        public override void SetupView()
        {
            base.SetupView();
            var view = new ForecastsChart
            {
                DataContext = ViewModel
            };

            ContextMenuElementName = nameof(view.HiddenContextMenu);
            ViewElement = view;
        }

        protected abstract IForecastConnection GetForecastConnection();

        protected abstract IFormater GetTimeFormater();
    }
}