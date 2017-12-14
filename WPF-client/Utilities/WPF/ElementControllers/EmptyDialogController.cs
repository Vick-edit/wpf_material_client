using System.Windows;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    public class EmptyDialogController : BaseNotifyPropertyChanged, IDialogController
    {
        public FrameworkElement DialogElement { get; }
        public bool IsDialogShown { get; set; }

        public EmptyDialogController()
        {
            DialogElement = null;
            IsDialogShown = false;
        }
    }
}