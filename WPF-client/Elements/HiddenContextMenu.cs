using System.Windows;
using MaterialDesignThemes.Wpf;

namespace WPF_client.Elements
{
    /// <summary> Меню для страницы, данные из которого будут подтянуты в меню верхнего уровня, но само по себе меню будет скрыто на странице </summary>
    public class HiddenContextMenu : PopupBox
    {
        public HiddenContextMenu()
            : base()
        {
            base.StaysOpen = false;
            base.PlacementMode = PopupBoxPlacementMode.BottomAndAlignRightEdges;
            this.Visibility = Visibility.Collapsed;
        }


        public new static readonly DependencyProperty StyleProperty = DependencyProperty.Register(
            nameof(Style), typeof (Style), typeof (HiddenContextMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));


        public new static readonly DependencyProperty StaysOpenProperty = DependencyProperty.Register(
            nameof(StaysOpen), typeof (bool), typeof (HiddenContextMenu), new PropertyMetadata(default(bool)));


        public new static readonly DependencyProperty PlacementModeProperty = DependencyProperty.Register(
            nameof(PlacementMode), typeof (PopupBoxPlacementMode), typeof (HiddenContextMenu),
            new PropertyMetadata(default(PopupBoxPlacementMode)));


        public new Style Style
        {
            get { return base.Style; }
            set { }
        }

        public new bool StaysOpen
        {
            get { return base.StaysOpen; }
            set { }
        }

        public new PopupBoxPlacementMode PlacementMode
        {
            get { return base.PlacementMode; }
            set { }
        }
    }
}