using System;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices;
using WPF_client.DomainServices.ServerDataProviders;
using WPF_client.Elements;
using WPF_client.Utilities;
using WPF_client.Utilities.Formaters;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.View;
using WPF_client.ViewModel;
using WPF_client.ViewProduction.Interfaces;
using WPF_client.WPFServices.DataProviderWrappers;

namespace WPF_client.ViewProduction.Builders.Forecasts
{
    /// <summary> Шаблон построения страницы с прогнозами </summary>
    public abstract class BaseForecastPageBuilder : BasePageBuilder, IPageBuilder
    {
        /// <summary> Провайдер подписки на данные о прогнозах </summary>
        private IForecastProvider _forecastProvider;
        /// <summary> Элемент, позволяющий создавать csv файлы из набора данных </summary>
        private ICsvFileCreator _csvFileCreator;


        /// <summary> Задать бизнеслогику страницы </summary>
        public override void SetupBuisnesLogic()
        {
            base.SetupBuisnesLogic();

            var forecastConnection = GetForecastConnection();
            _forecastProvider = new ForecastProvider(forecastConnection, TimeSpan.FromMinutes(5));
            _csvFileCreator = new CsvFileCreator();
        }

        /// <summary> Построить VM для данной страницы </summary>
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

        /// <summary> Построить view данной страницы </summary>
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

        /// <summary> Объект, позволяющий получить данные от сервера </summary>
        protected abstract IGetSingleObjectRequest<ForecastBlock> GetForecastConnection();

        /// <summary> Форматтер для данных графика </summary>
        protected abstract IDateFormater GetTimeFormater();
    }
}