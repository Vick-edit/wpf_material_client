﻿using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts.Events;
using WPF_client.Extensions;
using WPF_client.Utilities;
using WPF_client.ViewModel;

namespace WPF_client.View
{
    /// <summary>
    /// Interaction logic for ForecastsChart.xaml
    /// </summary>
    public partial class ForecastsChart : UserControl
    {
        public ForecastsChart()
        {
            InitializeComponent();
            Background = Brushes.Transparent;
            this.HandleDisposableViewModel();
        }


        #region Services elements
        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (DataContext is MainChartViewModel)
                    throw new InvalidDataException("Неверный формат контекста страницы");
                base.DataContext = value;
                UpdateEventBindings();
            }
        }

        private MainChartViewModel ExtractViewModel()
        {
            var viewModel = DataContext as MainChartViewModel;
            if (viewModel == null)
                throw new Exception("Задан неверный контекст модели");
            return viewModel;
        }

        private void UpdateEventBindings()
        {
            var viewModel = ExtractViewModel();
            viewModel.OnMessage += ViewModelOnOnMessage;
        }
        #endregion

        #region EventHandlers
        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            /*var viewModel = ExtractViewModel();
            viewModel.UpdateFormatter(eventargs.Range);*/
        }

        private void Axis_OnPreviewRangeChanged(PreviewRangeChangedEventArgs e)
        {
            //TODO: Решить проблему c ограничением диапозона на шкале. Он не может быть ограничеен при изменении со скролбара
            return;

            //TODO: Решить проблему, что при потере фокуса панинг работает некорректно
            var viewModel = ExtractViewModel();

            var percent = viewModel.MaxRange*0.3;
            if (e.PreviewMinValue < viewModel.MinValueX - percent)
                e.Cancel = true;
            if (e.PreviewMaxValue > viewModel.MaxValueX + percent)
                e.Cancel = true;
        }

        private void ViewModelOnOnMessage(object sender, string message)
        {
            Session.Instance.SnackbarMessageQueue.Enqueue(
                    message, "OK", () => { }
                );
        }
        #endregion
    }
}
