using System;
using LiveCharts;
using LiveCharts.Defaults;
using WPF_client.View;
using WPF_client.ViewModel;

namespace WPF_client.ViewProduction
{
    public class MainViewFactory : IViewFactory
    {
        /// <summary> График прогноза на месяц </summary>
        public MainChart GetMonthForecastView()
        {
            var values = new ChartValues<DateTimePoint>();

            var trend = 50d;
            var random = new Random();
            var timeStep = DateTime.Now;
            for (var i = 0; i < 30; i++)
            {
                timeStep = timeStep.AddHours(1);
                values.Add(new DateTimePoint(timeStep.AddDays(i), trend));

                if (random.NextDouble() > 0.4)
                {
                    trend += random.NextDouble() * 10;
                }
                else
                {
                    trend -= random.NextDouble() * 10;
                }
            }

            return new MainChart
            {
                DataContext = new MainChartViewModel(values)
            };
        }

        /// <summary> График прогноза на неделю </summary>
        public MainChart GetWeekForecast()
        {
            var values = new ChartValues<DateTimePoint>();

            var trend = 50d;
            var random = new Random();
            var timeStep = DateTime.Now;
            for (var i = 0; i < 7*24; i++)
            {
                timeStep = timeStep.AddHours(1);
                values.Add(new DateTimePoint(timeStep.AddHours(i), trend));

                if (random.NextDouble() > 0.4)
                {
                    trend += random.NextDouble() * 10;
                }
                else
                {
                    trend -= random.NextDouble() * 10;
                }
            }

            return new MainChart
            {
                DataContext = new MainChartViewModel(values)
            };
        }

        /// <summary> График прогноза на день </summary>
        public MainChart GetDayForecast()
        {
            var values = new ChartValues<DateTimePoint>();

            var trend = 50d;
            var random = new Random();
            var timeStep = DateTime.Now;
            for (var i = 0; i < 24*4; i++)
            {
                timeStep = timeStep.AddHours(1);
                values.Add(new DateTimePoint(timeStep.AddMinutes(i*15), trend));

                if (random.NextDouble() > 0.4)
                {
                    trend += random.NextDouble() * 10;
                }
                else
                {
                    trend -= random.NextDouble() * 10;
                }
            }

            return new MainChart
            {
                DataContext = new MainChartViewModel(values)
            };
        }


        /// <summary> Управление темами приложения </summary>
        public PaletteSelector GetPaletteSelectoView()
        {
            return new PaletteSelector
            {
                DataContext = new PaletteSelectorViewModel()
            };
        }
    }
}