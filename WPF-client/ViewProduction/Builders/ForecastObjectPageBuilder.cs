using System;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices;
using WPF_client.DomainServices.JsonDataSerialization;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;
using WPF_client.Elements;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction.Builders
{
    public class ForecastObjectPageBuilder : BasePageBuilder
    {
        private IGetCommand<ForecastJsonObject> _forecastObjectProvider;
        private ICsvFileCreator _csvFileCreator;

        public override void SetupBuisnesLogic()
        {
            base.SetupBuisnesLogic();

            var forecastDeserializer = new ForecastObjectDeserializer();
            _forecastObjectProvider = new GetForecastObjects(forecastDeserializer);
        }

        public override void SetupViewModel()
        {
            base.SetupViewModel();

            if (_forecastObjectProvider == null)
                throw new NullReferenceException("Не задана бизнеслогика страницы");

            var dialogElement = new ConnectionError();
            DialogController = new DialogController(dialogElement);

            ViewModel = new ForecastsObjectsViewModel(_forecastObjectProvider, DialogController);
        }

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