using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Transitions;

namespace WPF_client.Elements
{
    /// <summary> 
    /// Надстройка над ContentControl, на которой будет храниться контент, выбранный в левом меню,
    /// позволяет осуществить анимацию контента в соответствии с работой меню
    /// </summary>
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftClosedStateName)]
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftOpenStateName)]
    [TemplatePart(Name = TopElement, Type = typeof(FrameworkElement))]
    class LeftMenuContentWrapper : ContentControl
    {
        public const string TemplateLeftDrawerGroupName = "LeftDrawer";
        public const string TemplateLeftClosedStateName = "LeftDrawerClosed";
        public const string TemplateLeftOpenStateName = "LeftDrawerOpen";

        public const string TopElement = "PART_TopElement";
        public const string ZoomableElement = "PART_ZoomableElement";


        static LeftMenuContentWrapper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftMenuContentWrapper), new FrameworkPropertyMetadata(typeof(LeftMenuContentWrapper)));
        }


        #region TopElement

        public static readonly DependencyProperty TopElementContentProperty = DependencyProperty.Register(
            nameof(TopElementContent), typeof(object), typeof(LeftMenuContentWrapper), new PropertyMetadata(default(object)));

        public object TopElementContent
        {
            get { return (object)GetValue(TopElementContentProperty); }
            set { SetValue(TopElementContentProperty, value); }
        }

        public static readonly DependencyProperty TopElementContentTemplateProperty = DependencyProperty.Register(
            nameof(TopElementContentTemplate), typeof(DataTemplate), typeof(LeftMenuContentWrapper), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate TopElementContentTemplate
        {
            get { return (DataTemplate)GetValue(TopElementContentTemplateProperty); }
            set { SetValue(TopElementContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopElementContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(TopElementContentTemplateSelector), typeof(DataTemplateSelector), typeof(LeftMenuContentWrapper), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector TopElementContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TopElementContentTemplateSelectorProperty); }
            set { SetValue(TopElementContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty TopElementContentStringFormatProperty = DependencyProperty.Register(
            nameof(TopElementContentStringFormat), typeof(string), typeof(LeftMenuContentWrapper), new PropertyMetadata(default(string)));

        public string TopElementContentStringFormat
        {
            get { return (string)GetValue(TopElementContentStringFormatProperty); }
            set { SetValue(TopElementContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty TopElementBackgroundProperty = DependencyProperty.Register(
            nameof(TopElementBackground), typeof(Brush), typeof(LeftMenuContentWrapper), new PropertyMetadata(default(Brush)));

        public Brush TopElementBackground
        {
            get { return (Brush)GetValue(TopElementBackgroundProperty); }
            set { SetValue(TopElementBackgroundProperty, value); }
        }

        #endregion


        public static readonly DependencyProperty LeftMenuSizeProperty = DependencyProperty.Register(
            nameof(LeftMenuSize), typeof(double), typeof(LeftMenuContentWrapper), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double LeftMenuSize
        {
            get { return (double)GetValue(LeftMenuSizeProperty); }
            set { SetValue(LeftMenuSizeProperty, value); }
        }

        public static readonly DependencyProperty IsLeftMenuOpenProperty = DependencyProperty.Register(
            nameof(IsLeftMenuOpen), typeof(bool), typeof(LeftMenuContentWrapper), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsLeftMenuOpenPropertyChangedCallback));

        public bool IsLeftMenuOpen
        {
            get { return (bool)GetValue(IsLeftMenuOpenProperty); }
            set { SetValue(IsLeftMenuOpenProperty, value); }
        }


        public static readonly DependencyProperty IsSlideMenuProperty = DependencyProperty.Register(
            nameof(IsSlideMenu), typeof(bool), typeof(LeftMenuContentWrapper), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsSlideMenu
        {
            get { return (bool)GetValue(IsSlideMenuProperty); }
            set { SetValue(IsSlideMenuProperty, value); }
        }

        private static void IsLeftMenuOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var leftMenuContentWrapper = dependencyObject as LeftMenuContentWrapper;
            leftMenuContentWrapper?.UpdateVisualStates();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateVisualStates(false);
        }

        private void UpdateVisualStates(bool? useTransitions = null)
        {
            VisualStateManager.GoToState(this,
                IsLeftMenuOpen ? TemplateLeftOpenStateName : TemplateLeftClosedStateName,
                useTransitions.HasValue ? useTransitions.Value : !TransitionAssist.GetDisableTransitions(this));
        }
    }
}