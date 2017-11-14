using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_client.ViewModel
{
    public class MainChartViewModel
    {
        public SeriesCollection DataCollection { get; }

        public MainChartViewModel()
        {
            DataCollection = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<double>() { 3, 5, 7, 4 }
                },
                new ColumnSeries()
                {
                    Values = new ChartValues<double>() { 5, 6, 2, 7 }
                }
            };
        }
    }
}