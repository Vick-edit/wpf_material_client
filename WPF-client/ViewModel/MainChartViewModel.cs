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
        public object Mapper { get; set; }

        public Func<double, string> Formatter
        {
            get { return Get<Func<double, string>>(); }
            set { Set(value); }
        }

        public double MaxRange { get { return (MaxValueX - MinValueX) *1.1; } }
        public double MinValueX { get { return Values.Min(x => x.DateTime).Ticks; } }
        public double MaxValueX { get { return Values.Max(x => x.DateTime).Ticks; } }


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

        public MainChartViewModel()
        {
            Values = new ChartValues<DateTimePoint>();

            var trend = 50d;
            var random = new Random();
            var timeStep = DateTime.Now;
            for (var i = 0; i < 500; i++)
            {
                timeStep = timeStep.AddHours(1);
                Values.Add(new DateTimePoint(timeStep.AddDays(i*10), trend));

                if (random.NextDouble() > 0.4)
                {
                    trend += random.NextDouble() * 10;
                }
                else
                {
                    trend -= random.NextDouble() * 10;
                }
            }

            Formatter = x => new DateTime((long)x).ToString("yyyy");
            From = DateTime.Now.AddHours(10000).Ticks;
            To = DateTime.Now.AddHours(90000).Ticks;
        }

        public ChartValues<DateTimePoint> Values { get; set; }
    }
}