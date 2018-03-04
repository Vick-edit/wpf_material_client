using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Configurations;
using Microsoft.Win32;
using WPF_client.Domain.DomainModels;
using WPF_client.DomainServices;
using WPF_client.DomainServices.Exceptions;
using WPF_client.Utilities;
using WPF_client.Utilities.Formaters;
using WPF_client.Utilities.WPF.Commands;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.WPFServices.DataProviderWrappers;
using WPF_client.WPFServices.Events;

namespace WPF_client.ViewModel
{
    public class MainChartViewModel : ViewModelBase, INotifyPropertyChanged, IDisposable
    {
        public event EventWithMessage OnMessage;

        public CartesianMapper<Forecast> ForecastMapper
        {
            get { return Get<CartesianMapper<Forecast>>(); }
            set { Set(value); }
        }
        public CartesianMapper<Forecast> MeasurementsMapper
        {
            get { return Get<CartesianMapper<Forecast>>(); }
            set { Set(value); }
        }
        public CartesianMapper<Forecast> AllDataMapper
        {
            get { return Get<CartesianMapper<Forecast>>(); }
            set { Set(value); }
        }

        private readonly Func<double, string> valueFormatter = y => y + " КВт";
        private readonly Func<double, string> dateFormatter;
        private readonly Func<double, string> simpleDateFormatter;

        private const double RangeMaxScale = 1.1;
        private readonly double _startScale;

        private readonly IForecastProvider _forecastProvider;
        private readonly IDialogController _dialogController;
        private readonly ICsvFileCreator _csvFileCreator;

        private readonly long _timeSpanTicks;
        public MainChartViewModel(IForecastProvider forecastProvider, IDialogController dialogController, 
            ICsvFileCreator csvFileCreator, IDateFormater dateFormater, TimeSpan timeSpan)
        {
            //Один шаг зума увеличивает на 0,8 текущего диапозона, отсчитаем 3 зума назад
            _startScale = Math.Round(RangeMaxScale/1.8/1.8, 3);
            _timeSpanTicks = timeSpan.Ticks;

            IsDataSated = false;
            _dialogController = dialogController;
            _csvFileCreator = csvFileCreator;

            _forecastProvider = forecastProvider;
            _forecastProvider.OnForecastUpdated += OnForecastUpdated;
            _forecastProvider.OnConnectionLost += OnConnectionLosted;
            _forecastProvider.OnConnectionRestored += OnConnectionRestored;

            dateFormatter = dateFormater.DateFormatter;
            simpleDateFormatter = dateFormater.SimpleDateFormatter;


            ForecastMapper = Mappers.Xy<Forecast>()
                .X(item => item.ForecastTime.Ticks)
                .Y(item => item.IsForecast ? item.ForecastPower : Double.NaN);
            MeasurementsMapper = Mappers.Xy<Forecast>()
                .X(item => item.ForecastTime.Ticks)
                .Y(item => !item.IsForecast ? item.ForecastPower : item.ForecastPower);
            AllDataMapper = Mappers.Xy<Forecast>()
                .X(item => item.ForecastTime.Ticks)
                .Y(item => item.ForecastPower);
        }


        #region ViewModelFields
        public double MaxRange { get { return (MaxValueX - MinValueX) * RangeMaxScale; } }
        public double MinRange
        {
            get
            {
                var minValue = MinValueX;
                var nexValue = MaxValueX;
                if (IsDataSated)
                {
                    nexValue = ChartForecastValues.Where(x => x.ForecastTime.Ticks > minValue)
                        .Min(x => x.ForecastTime).Ticks;
                }
                return (nexValue - minValue) * 2;
            }
        }

        public long MaxValueX
        {
            get
            {
                if (!IsDataSated)
                    return DateTime.Now.AddMinutes(1).Ticks;
                return ChartForecastValues.Max(x => x.ForecastTime).Ticks;
            }
        }
        public long MinValueX
        {
            get
            {
                if (!IsDataSated)
                    return DateTime.Now.Ticks;
                return ChartForecastValues.Min(x => x.ForecastTime).Ticks;
            }
        }

        public double MaxValueY
        {
            get
            {
                if (!IsDataSated)
                    return double.MaxValue;
                if (ChartForecastValues.Max(x => x.ForecastPower) - ChartForecastValues.Min(x => x.ForecastPower) < 1)
                {
                    return ChartForecastValues.Max(x => x.ForecastPower) + 1;
                }
                return ChartForecastValues.Max(x => x.ForecastPower)*(1 + (1 - ChartForecastValues.Min(x => x.ForecastPower)/ChartForecastValues.Max(x => x.ForecastPower)) *0.15);
            }
        }
        public double MinValueY
        {
            get
            {
                if (!IsDataSated)
                    return double.MinValue;
                if (ChartForecastValues.Max(x => x.ForecastPower) - ChartForecastValues.Min(x => x.ForecastPower) < 1)
                {
                    return ChartForecastValues.Min(x => x.ForecastPower) - 1;
                }
                return ChartForecastValues.Min(x => x.ForecastPower)*(1-(ChartForecastValues.Max(x => x.ForecastPower)/ ChartForecastValues.Min(x => x.ForecastPower) - 1) *0.15);
            }
        }

        public ChartValues<Forecast> ChartForecastValues
        {
            get
            {
                if(!IsDataSated)
                    return new ChartValues<Forecast>();
                return Get<ChartValues<Forecast>>();
            }
            set
            {
                Set(value);
                UpdateAxisParameters();
                OnPropertyChanged(nameof(MaxRange));
                OnPropertyChanged(nameof(MinRange));
                OnPropertyChanged(nameof(MaxValueX));
                OnPropertyChanged(nameof(MinValueX));
                OnPropertyChanged(nameof(MaxValueY));
                OnPropertyChanged(nameof(MinValueY));
                OnPropertyChanged(nameof(MeasurementsValues));
                OnPropertyChanged(nameof(ForecastValues));
                OnPropertyChanged(nameof(Consumption));
                OnPropertyChanged(nameof(ConsumptionDate));
            }
        }

        public ChartValues<Forecast> MeasurementsValues
        {
            get
            {
                if (!IsDataSated)
                    return new ChartValues<Forecast>();
                return Get<ChartValues<Forecast>>();
            }
            set
            {
                Set(value);
            }
        }

        public ChartValues<Forecast> ForecastValues
        {
            get
            {
                if (!IsDataSated)
                    return new ChartValues<Forecast>();
                return Get<ChartValues<Forecast>>();
            }
            set
            {
                Set(value);
            }
        }

        public double Consumption
        {
            get
            { 
                return Get<double>();
            }
            set
            {
                Set(value);
            }
        }

        public string ConsumptionDate
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public double From
        {
            get
            {
                if (!IsDataSated)
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
                if (!IsDataSated)
                    return MaxValueX;
                return Get<double>();
            }
            set { Set(value); }
        }

        public bool IsDataSated
        {
            get { return Get<bool>(); }
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
            IsDataSated = false;
        }

        [MapCommand(nameof(SaveDataToCsv))]
        public ICommand SaveToCsvCommand { get; private set; }

        public Func<double, string> ValueFormatter
        {
            get
            {
                return valueFormatter;
            }
        }

        public Func<double, string> DateFormatter
        {
            get
            {
                return dateFormatter;
            }
        }

        public Func<double, string> SimpleDateFormatter
        {
            get
            {
                return simpleDateFormatter;
            }
        }

        private void SaveDataToCsv()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == true)
                {
                    var filePath = saveFileDialog.FileName;
                    var selectedForecasts = GetForecastsForPeriod(_forecastProvider.ForecastsBlock.Forecasts);
                    var saved = _csvFileCreator.SaveToFile(filePath, selectedForecasts);
                    var message = saved ? "Файл успешно сохранен" : "Файл не был сохранён";
                    OnMessage?.Invoke(this, message);
                }
            }
            catch (Exception e)
            {
                ExceptionLogger.Log(e);

                var errorDescription = $"Не удалось сохранить файл, возникла следующая ошибка:\r\n{e.Message}";
                OnMessage?.Invoke(this, errorDescription);
            }
        }
        #endregion


        /*/// <summary> Обновить подписи по оси X в соответствии с масштабом </summary>
        public void UpdateFormatter(double currentRange)
        {
            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                DateFormatter = x => new DateTime((long)x).ToString("t");
                return;
            }
            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                DateFormatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }
            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                DateFormatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }
            DateFormatter = x => new DateTime((long)x).ToString("yyyy");
        }*/

        private void UpdateAxisParameters()
        {
            if (!ChartForecastValues.Any())
                throw new Exception("Не заданна коллеция значений графика");
            if (ChartForecastValues.Count < 2)
                throw new Exception("Размер коллекции менее двух значений не может быть корректно отображен");

            From = MinValueX;
            To = MinValueX + (MaxValueX - MinValueX) * _startScale;

            //UpdateFormatter(To - From);
        }

        private List<Forecast> GetForecastsForPeriod(IList<Forecast> forecasts)
        {
            return forecasts.ToList();
            var minDate = forecasts.Min(x => x.ForecastTime);
            var maxDate = minDate.AddTicks(_timeSpanTicks);
            var selectedForecasts = forecasts.Where(x => x.ForecastTime < maxDate).ToList();
            return selectedForecasts;
        }


        #region EventHandlers
        private void OnForecastUpdated(object sender, ForecastBlock forecastsBlock)
        {
            IsDataSated = true;
            _dialogController.IsDialogShown = false;
            var forecasts = forecastsBlock.Forecasts;
            /*var selectedForecasts = GetForecastsForPeriod(forecasts);
            var forecastsAmount = selectedForecasts.Count;

            var chartValues = new ChartValues<Forecast>();
            var realValues = new ChartValues<DateTimePoint>();
            var forecastValues = new ChartValues<DateTimePoint>();
            for (var i = 0; i < forecastsAmount && i < 500; i++)
            {
                chartValues.Add(selectedForecasts[i]);
                var forecast = selectedForecasts[i];
                chartValues.Add(new DateTimePoint(forecast.ForecastTime, forecast.ForecastPower));

                if (forecast.IsForecast)
                    forecastValues.Add(new DateTimePoint(forecast.ForecastTime, forecast.ForecastPower));
                else
                    realValues.Add(new DateTimePoint(forecast.ForecastTime, forecast.ForecastPower));
            }

            var firstForecastDate = forecastValues.Min(x => x.DateTime);
            var firstForecast = realValues.Where(x => x.DateTime < firstForecastDate).OrderByDescending(x => x.DateTime).FirstOrDefault();
            forecastValues.Insert(0, firstForecast);

            AllValues = chartValues;
            RealValues = realValues;
            ForecastValues = forecastValues;*/

            var allDataValue = new ChartValues<Forecast>();
            var measurementsValue = new ChartValues<Forecast>();
            var forecastsValue = new ChartValues<Forecast>();
            var prevForecast = forecasts.First();
            var isPreWasForecast = prevForecast.IsForecast;

            for (var i = 0; i < forecasts.Count; i++)
            {
                var currentForecastData = forecasts[i];
                var isForecastNow = currentForecastData.IsForecast;

                allDataValue.Add(currentForecastData);
                if (isForecastNow)
                    forecastsValue.Add(currentForecastData);
                else
                    measurementsValue.Add(currentForecastData);

                if (isForecastNow != isPreWasForecast)
                {
                    if (isPreWasForecast)
                        measurementsValue.Add(prevForecast);
                    else
                        measurementsValue.Add(currentForecastData);
                }

                isPreWasForecast = isForecastNow;
                prevForecast = currentForecastData;
            }
            Consumption = forecastsBlock.Consumption;
            ConsumptionDate = forecastsValue.Last().ForecastTime.ToString("dd.MMM.yy");
            ForecastValues = forecastsValue;
            MeasurementsValues = measurementsValue;
            ChartForecastValues = allDataValue;
        }

        private void OnConnectionLosted(object sender, ConnectionException updateError)
        {
            IsDataSated = false;
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