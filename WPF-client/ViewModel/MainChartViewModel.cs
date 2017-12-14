using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Defaults;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices.ConnectionProviders;
using WPF_client.DomainServices.Exceptions;
using WPF_client.Utilities.WPF.Commands;
using WPF_client.Utilities.WPF.ElementControllers;

namespace WPF_client.ViewModel
{
    public class MainChartViewModel : ViewModelBase, INotifyPropertyChanged, IDisposable
    {
        private bool _isDataNotSated;
    
        private const double RangeMaxScale = 1.1;
        private readonly double _startScale;

        private readonly IForecastProvider _forecastProvider;
        private readonly IDialogController _dialogController;

        private readonly long _timeSpanTicks;
        public MainChartViewModel(IForecastProvider forecastProvider, IDialogController dialogController, TimeSpan timeSpan)
        {
            //Один шаг зума увеличивает на 0,8 текущего диапозона, отсчитаем 3 зума назад
            _startScale = Math.Round(RangeMaxScale/1.8/1.8, 3);
            _timeSpanTicks = timeSpan.Ticks;

            _isDataNotSated = true;
            _dialogController = dialogController;

            _forecastProvider = forecastProvider;
            _forecastProvider.OnForecastUpdated += OnForecastUpdated;
            _forecastProvider.OnConnectionLost += OnConnectionLosted;
            _forecastProvider.OnConnectionRestored += OnConnectionRestored;
        }


        #region ViewModelFields
        public double MaxRange { get { return (MaxValueX - MinValueX) * RangeMaxScale; } }
        public double MinRange
        {
            get
            {
                var minValue = MinValueX;
                var nexValue = MaxValueX;
                if (!_isDataNotSated)
                {
                    nexValue = Values.Where(x => x.DateTime.Ticks > minValue)
                        .Min(x => x.DateTime).Ticks;
                }
                return (nexValue - minValue) * 2;
            }
        }

        public long MaxValueX
        {
            get
            {
                if (_isDataNotSated)
                    return DateTime.Now.AddMinutes(1).Ticks;
                return Values.Max(x => x.DateTime).Ticks;
            }
        }
        public long MinValueX
        {
            get
            {
                if (_isDataNotSated)
                    return DateTime.Now.Ticks;
                return Values.Min(x => x.DateTime).Ticks;
            }
        }


        public ChartValues<DateTimePoint> Values
        {
            get
            {
                if(_isDataNotSated)
                    return new ChartValues<DateTimePoint>();
                return Get<ChartValues<DateTimePoint>>();
            }
            set
            {
                Set(value);
                UpdateAxisParameters();
                OnPropertyChanged(nameof(MaxRange));
                OnPropertyChanged(nameof(MinRange));
                OnPropertyChanged(nameof(MaxValueX));
                OnPropertyChanged(nameof(MinValueX));
            }
        }

        public Func<double, string> Formatter
        {
            get
            {
                if (_isDataNotSated)
                    return x => new DateTime((long)x).ToString("t");
                return Get<Func<double, string>>();
            }
            set { Set(value); }
        }

        public double From
        {
            get
            {
                if (_isDataNotSated)
                    return MinValueX;
                return Get<double>();
            }
            set
            {
                Set(value);
            }
        }
        public double To
        {
            get
            {
                if (_isDataNotSated)
                    return MaxValueX;
                return Get<double>();
            }
            set { Set(value); }
        }
        #endregion


        #region Commands
        [MapCommand(nameof(StartWatchingForUpdates))]
        public ICommand StartConnection { get; private set; }
        private void StartWatchingForUpdates()
        {
            _forecastProvider.StartWatchingForUpdates();
        }

        [MapCommand(nameof(StopWatchingForUpdates))]
        public ICommand StopConnection { get; private set; }
        private void StopWatchingForUpdates()
        {
            _forecastProvider.StopWatchingForUpdates();
            _isDataNotSated = true;
        }
        #endregion


        /// <summary> Обновить подписи по оси X в соответствии с масштабом </summary>
        public void UpdateFormatter(double currentRange)
        {
            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }
            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }
            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }
            Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        private void UpdateAxisParameters()
        {
            if (!Values.Any())
                throw new Exception("Не заданна коллеция зачений графика");
            if (Values.Count < 2)
                throw new Exception("Размер коллекции менее двух значений не может быть корректно отображен");

            From = MinValueX;
            To = MinValueX + (MaxValueX - MinValueX) * _startScale;

            UpdateFormatter(To - From);
        }


        #region EventHandlers
        private void OnForecastUpdated(object sender, IList<Forecast> forecasts)
        {
            _isDataNotSated = false;
            _dialogController.IsDialogShown = false;
            var minDate = forecasts.Min(x => x.ForecastTime);
            var maxDate = minDate.AddTicks(_timeSpanTicks);
            var selectedForecasts = forecasts.Where(x => x.ForecastTime < maxDate).ToList();

            var chartValues = new ChartValues<DateTimePoint>();
            for (var i = 0; i < selectedForecasts.Count && i < 500; i++)
            {
                var forecast = selectedForecasts[i];
                chartValues.Add(new DateTimePoint(forecast.ForecastTime, forecast.ForecastPower));
            }

            Values = chartValues;
        }

        private void OnConnectionLosted(object sender, ConnectionException updateError)
        {
            _isDataNotSated = true;
            _dialogController.IsDialogShown = true;
        }

        private void OnConnectionRestored(object sender, string message)
        {
            _dialogController.IsDialogShown = false;
        }
        #endregion


        public void Dispose()
        {
            _forecastProvider?.Dispose();
        }
    }
}