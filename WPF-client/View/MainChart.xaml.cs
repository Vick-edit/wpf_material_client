using System;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts.Events;
using WPF_client.ViewModel;

namespace WPF_client.View
{
    /// <summary>
    /// Interaction logic for MainChart.xaml
    /// </summary>
    public partial class MainChart : UserControl
    {
        public MainChart()
        {
            InitializeComponent();
            Background = Brushes.Transparent;
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var viewModel = (MainChartViewModel)DataContext;

            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                viewModel.Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }

            viewModel.Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        private void Axis_OnPreviewRangeChanged(PreviewRangeChangedEventArgs e)
        {
            var vm = (MainChartViewModel)DataContext;

            var percent = vm.MaxRange*0.3;
            if (e.PreviewMinValue < vm.MinValueX - percent)
                e.Cancel = true;
            if (e.PreviewMaxValue > vm.MaxValueX + percent)
                e.Cancel = true;
        }
    }
}
