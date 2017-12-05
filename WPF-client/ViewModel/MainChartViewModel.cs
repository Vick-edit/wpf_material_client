using System;
using System.ComponentModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.ViewModel
{
    public class MainChartViewModel : BaseNotifyPropertyChanged, INotifyPropertyChanged
    {
        private const double RangeMaxScale = 1.1;
        private readonly double StartScale;

        public MainChartViewModel(ChartValues<DateTimePoint> chartValues)
        {
            //Один шаг зума увеличивает на 0,8 текущего диапозона, отсчитаем 3 зума назад
            StartScale = Math.Round(RangeMaxScale/1.8/1.8, 3);

            Values = chartValues;
            UpdateAxisParameters();
        }


        #region ViewModelFields
        public double MaxRange { get { return (MaxValueX - MinValueX) * RangeMaxScale; } }
        public double MinRange
        {
            get
            {
                var minValue = MinValueX;
                var nexValue = Values.Where(x => x.DateTime.Ticks > minValue)
                    .Min(x => x.DateTime).Ticks;
                return (nexValue - minValue) * 2;
            }
        }

        public long MaxValueX { get { return Values.Max(x => x.DateTime).Ticks; } }
        public long MinValueX { get { return Values.Min(x => x.DateTime).Ticks; } }


        public ChartValues<DateTimePoint> Values
        {
            get { return Get<ChartValues<DateTimePoint>>(); }
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
            get { return Get<Func<double, string>>(); }
            set { Set(value); }
        }

        public double From
        {
            get { return Get<double>(); }
            set { Set(value); }
        }
        public double To
        {
            get { return Get<double>(); }
            set { Set(value); }
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
            To = MinValueX + (MaxValueX - MinValueX) * StartScale;

            UpdateFormatter(To - From);
        }
    }
}