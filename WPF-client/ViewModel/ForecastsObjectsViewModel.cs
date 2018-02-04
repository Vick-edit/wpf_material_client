using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPF_client.Domain.DomainModels;
using WPF_client.Domain.ServerConnection;
using WPF_client.DomainServices.JsonDataSerialization.MapingObjects;
using WPF_client.Utilities;
using WPF_client.Utilities.WPF.ElementControllers;

namespace WPF_client.ViewModel
{
    public class ForecastsObjectsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IGetCommand<ForecastJsonObject> _forecastObjectProvider;
        private readonly IDialogController _dialogController;

        public ForecastsObjectsViewModel(IGetCommand<ForecastJsonObject> forecastObjectProvider, IDialogController dialogController)
        {
            _forecastObjectProvider = forecastObjectProvider;
            _dialogController = dialogController;
        }

        private List<ForecastJsonObject> _forecastObjects;
        public List<ForecastJsonObject> ForecastObjects
        {
            get
            {
                if (_forecastObjects == null)
                {
                    _forecastObjects = GetForecastsObjects();
                    SelectedItem = _forecastObjects?.FirstOrDefault();
                }
                return _forecastObjects;
            }
            set
            {
                if (value != _forecastObjects)
                {
                    _forecastObjects = value;
                    SelectedItem = _forecastObjects?.FirstOrDefault();
                    OnPropertyChanged(nameof(ForecastObjects));
                }
            }
        }

        public ForecastJsonObject SelectedItem
        {
            get { return Get<ForecastJsonObject>(); }
            set
            {
                Set(value);
                Session.Instance.ActiveForecastObjectId = value.id;
            }
        }

        private List<ForecastJsonObject> GetForecastsObjects()
        {
            IList<ForecastJsonObject> forecastObjects = null;

            try
            {
                forecastObjects = _forecastObjectProvider.GetDataFromServer();
            }
            catch (Exception e)
            {
                ExceptionLogger.Log(e);
                ConnectionError();
                Application.Current.Dispatcher.BeginInvoke(new ThreadStart(SetUpAfterError));
            }

            return forecastObjects?.ToList();
        }

        private void ConnectionError(bool show = true)
        {
            _dialogController.IsDialogShown = show;
        }

        private void SetUpAfterError()
        {
            IList<ForecastJsonObject> forecastObjects = null;
            var isError = true;
            while (isError)
            {
                try
                {
                    forecastObjects = _forecastObjectProvider.GetDataFromServer();
                    isError = false;
                }
                catch (Exception e)
                {
                    ExceptionLogger.Log(e);
                }
            }

            ConnectionError(false);
            ForecastObjects = forecastObjects?.ToList();
        }
    }
}