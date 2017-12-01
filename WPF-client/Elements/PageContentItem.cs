using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Elements
{
    public class PageContentItem : BaseNotifyPropertyChanged, INotifyPropertyChanged
    {
        private string _name;
        private object _content;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(16);

        public PageContentItem(string name, object content)
        {
            _name = name;
            Content = content;
        }

        public string Name
        {
            get { return _name; }
            set { this.ChangeProperty(ref _name, value, RaisePropertyChanged()); }
        }

        public object Content
        {
            get { return _content; }
            set { this.ChangeProperty(ref _content, value, RaisePropertyChanged()); }
        }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get { return _horizontalScrollBarVisibilityRequirement; }
            set { this.ChangeProperty(ref _horizontalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get { return _verticalScrollBarVisibilityRequirement; }
            set { this.ChangeProperty(ref _verticalScrollBarVisibilityRequirement, value, RaisePropertyChanged()); }
        }

        public Thickness MarginRequirement
        {
            get { return _marginRequirement; }
            set { this.ChangeProperty(ref _marginRequirement, value, RaisePropertyChanged()); }
        }
    }
}