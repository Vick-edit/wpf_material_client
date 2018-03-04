using System;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;
using WPF_client.DomainServices.ServerDataProviders;
using WPF_client.Elements;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction.Builders
{
    /// <summary> Базовый строитель для страниц с прогнозами </summary>
    public class ForecastObjectPageBuilder : BasePageBuilder
    {
        /// <summary> Провайдер данных об объектах прогнозирвоания с сервера </summary>
        private IGetListRequest<ForecastObject> _forecastObjectProvider;


        /// <summary> Задать бизнеслогику страницы </summary>
        public override void SetupBuisnesLogic()
        {
            base.SetupBuisnesLogic();

            var forecastDeserializer = new ForecastObjectDeserializer();
            _forecastObjectProvider = new GetForecastObjects(forecastDeserializer);
        }

        /// <summary> Построить VM для данной страницы </summary>
        public override void SetupViewModel()
        {
            base.SetupViewModel();

            if (_forecastObjectProvider == null)
                throw new NullReferenceException("Не задана бизнеслогика страницы");

            var dialogElement = new ConnectionError();
            DialogController = new DialogController(dialogElement);

            ViewModel = new ForecastsObjectsViewModel(_forecastObjectProvider, DialogController);
        }

        /// <summary> Построить view данной страницы </summary>
        public override void SetupView()
        {
            base.SetupView();
            ViewElement = new ForecastsObjects
            {
                DataContext = ViewModel
            };
        }
    }
}