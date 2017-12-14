using System.Windows;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    public class DialogController : BaseNotifyPropertyChanged, IDialogController
    {
        private FrameworkElement _dialogElement;
        private bool _isDialogShown;

        public FrameworkElement DialogElement
        {
            get { return _dialogElement; }
            set { this.ChangeProperty(ref _dialogElement, value, RaisePropertyChanged()); }
        }

        public bool IsDialogShown
        {
            get { return _isDialogShown; }
            set
            {
                this.ChangeProperty(ref _isDialogShown, value, RaisePropertyChanged());
            }
        }

        public DialogController(FrameworkElement dialogElement)
        {
            DialogElement = dialogElement;
            IsDialogShown = false;
        }
    }
}