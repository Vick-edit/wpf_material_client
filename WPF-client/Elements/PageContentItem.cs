using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Elements
{
    /// <summary> Врапер контента любой страницы приложения для левого меню </summary>
    public class PageContentItem : BaseNotifyPropertyChanged, INotifyPropertyChanged
    {
        private StackPanel _contextElements;
        private Thickness _marginRequirement = new Thickness(16);


        public PageContentItem(string name, object content, IDialogController dialogController)
            :this(name, content, dialogController, null) { }

        public PageContentItem(string name, object content, IDialogController dialogController, StackPanel contextElements)
        {
            IsActive = true;
            Name = name;
            Content = content;

            DialogController = dialogController;
            ContextElements = contextElements;
        }


        #region Данные формы
        /// <summary> Доступна ли форма в меню </summary>
        public bool IsActive
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        /// <summary> Имя формы в меню </summary>
        public string Name
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        /// <summary> Содержимое формы </summary>
        public object Content
        {
            get { return Get<object>(); }
            set { Set(value); }
        }

        /// <summary> Контрол для диалога об ошибках и прочего </summary>
        public IDialogController DialogController
        {
            get { return Get<IDialogController>(); }
            set { Set(value); }
        }

        /// <summary> Контекстное меню формы </summary>
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
        #endregion


        #region Графическое представление формы
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
            get { return Get<ScrollBarVisibility>(); }
            set { Set(value); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get { return Get<ScrollBarVisibility>(); }
            set { Set(value); }
        }

        public Thickness MarginRequirement
        {
            get { return _marginRequirement; }
            set { this.ChangeProperty(ref _marginRequirement, value, RaisePropertyChanged()); }
        } 
        #endregion
    }
}