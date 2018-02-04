using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_client.View
{
    /// <summary>
    /// Interaction logic for ForecastsObjects.xaml
    /// </summary>
    public partial class ForecastsObjects : UserControl
    {
        private object _dataContext;
        private bool loaded;

        public ForecastsObjects()
        {
            InitializeComponent();
            Background = Brushes.Transparent;

            this.Loaded += (o, e) =>
            {
                loaded = true;
                Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        Thread.Sleep(500);
                        SetUpDataContext(this, _dataContext);
                    })
                );
            };
        }

        public new object DataContext
        {
            get { return base.DataContext; }
            set
            {
                if (loaded)
                    base.DataContext = value;
                else
                    _dataContext = value;
            }
        }

        private void SetUpDataContext(UserControl element, object dataContext)
        {
            element.DataContext = dataContext;
        }
    }
}
