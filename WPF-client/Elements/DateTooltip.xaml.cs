using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_client.Elements
{
    /// <summary>
    /// Interaction logic for DateTooltip.xaml - перегрузка DefaultTooltip
    /// </summary>
    public partial class DateTooltip : IChartTooltip
    {
        private TooltipData _data;

        /// <summary>
        /// Initializes a new instance of DefaultTooltip class
        /// </summary>
        public DateTooltip()
        {
            InitializeComponent();

            DataContext = this;
        }

        /// <summary>
        /// Initializes the <see cref="DefaultTooltip"/> class.
        /// </summary>
        static DateTooltip()
        {
            BackgroundProperty.OverrideMetadata(
                typeof(DateTooltip), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(140, 255, 255, 255))));
            PaddingProperty.OverrideMetadata(
                typeof(DateTooltip), new FrameworkPropertyMetadata(new Thickness(10, 5, 10, 5)));
            EffectProperty.OverrideMetadata(
                typeof(DateTooltip),
                new FrameworkPropertyMetadata(new DropShadowEffect { BlurRadius = 3, Color = Color.FromRgb(50, 50, 50), Opacity = .2 }));
        }

        /// <summary>
        /// The show title property
        /// </summary>
        public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register(
            "ShowTitle", typeof(bool), typeof(DateTooltip), new PropertyMetadata(true));
        /// <summary>
        /// Gets or sets a value indicating whether the tooltip should show the shared coordinate value in the current tooltip data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show title]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }

        /// <summary>
        /// The show series property
        /// </summary>
        public static readonly DependencyProperty ShowSeriesProperty = DependencyProperty.Register(
            "ShowSeries", typeof(bool), typeof(DateTooltip), new PropertyMetadata(true));
        /// <summary>
        /// Gets or sets a value indicating whether should show series name and color.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show series]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowSeries
        {
            get { return (bool)GetValue(ShowSeriesProperty); }
            set { SetValue(ShowSeriesProperty, value); }
        }

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(DateTooltip), new PropertyMetadata(new CornerRadius(4)));
        /// <summary>
        /// Gets or sets the corner radius of the tooltip
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// The selection mode property
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            "SelectionMode", typeof(TooltipSelectionMode?), typeof(DateTooltip),
            new PropertyMetadata(null));
        /// <summary>
        /// Gets or sets the tooltip selection mode, default is null, if this property is null LiveCharts will decide the selection mode based on the series (that fired the tooltip) preferred section mode
        /// </summary>
        public TooltipSelectionMode? SelectionMode
        {
            get { return (TooltipSelectionMode?)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        /// <summary>
        /// The bullet size property
        /// </summary>
        public static readonly DependencyProperty BulletSizeProperty = DependencyProperty.Register(
            "BulletSize", typeof(double), typeof(DateTooltip), new PropertyMetadata(15d));
        /// <summary>
        /// Gets or sets the bullet size, the bullet size modifies the drawn shape size.
        /// </summary>
        public double BulletSize
        {
            get { return (double)GetValue(BulletSizeProperty); }
            set { SetValue(BulletSizeProperty, value); }
        }

        /// <summary>
        /// The is wrapped property
        /// </summary>
        public static readonly DependencyProperty IsWrappedProperty = DependencyProperty.Register(
            "IsWrapped", typeof(bool), typeof(DateTooltip), new PropertyMetadata(default(bool)));
        /// <summary>
        /// Gets or sets whether the tooltip is shared in the current view, this property is useful to control
        /// the z index of a tooltip according to a set of controls in a container.
        /// </summary>
        public bool IsWrapped
        {
            get { return (bool)GetValue(IsWrappedProperty); }
            set { SetValue(IsWrappedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TooltipData Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
