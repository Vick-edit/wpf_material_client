using System.Windows;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    /// <summary> Диалоговое окно </summary>
    public class DialogController : BaseNotifyPropertyChanged, IDialogController
    {
        /// <summary> WPF, реализующий отображение сообщений </summary>
        public FrameworkElement DialogElement
        {
            get { return Get<FrameworkElement>(); }
            set { Set(value); }
        }

        /// <summary> Отображается ли сейчас диалоговое коно? </summary>
        public bool IsDialogShown
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public DialogController(FrameworkElement dialogElement)
        {
            DialogElement = dialogElement;
            IsDialogShown = false;
        }
    }
}