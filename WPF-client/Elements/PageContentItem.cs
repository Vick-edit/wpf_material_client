using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Elements
{
    public class PageContentItem : BaseNotifyPropertyChanged, INotifyPropertyChanged
    {
        private string _name;
        private object _content;
        private bool _isActive;
        private IDialogController _dialogController;
        private StackPanel _contextElements;
        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement;
        private Thickness _marginRequirement = new Thickness(16);


        public PageContentItem(string name, object content, IDialogController dialogController)
            :this(name, content, dialogController, null) { }

        public PageContentItem(string name, object content, IDialogController dialogController, StackPanel contextElements)
        {
            _isActive = true;
            _name = name;
            Content = content;

            DialogController = dialogController;
            ContextElements = contextElements;
        }


        public bool IsActive
        {
            get { return _isActive; }
            set { this.ChangeProperty(ref _isActive, value, RaisePropertyChanged()); }
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


        public IDialogController DialogController
        {
            get { return _dialogController; }
            set { this.ChangeProperty(ref _dialogController, value, RaisePropertyChanged()); }
        }

        public StackPanel ContextElements
        {
            get { return _contextElements; }
            set
            {
                _contextElements = value;
                OnPropertyChanged(nameof(ContextElements));
                OnPropertyChanged(nameof(ContextMenuVisibility));
            }
        }

        public Visibility ContextMenuVisibility
        {
            get
            {
                if (ContextElements != null)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
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