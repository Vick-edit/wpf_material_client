using System.Windows;
using WPF_client.Utilities.WPF.NotifyPropertyChanged;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    /// <summary> Диалоговое коно - заглушка </summary>
    public class EmptyDialogController : BaseNotifyPropertyChanged, IDialogController
    {
        /// <summary> Всегда null </summary>
        public FrameworkElement DialogElement { get; }

        /// <summary> Не на что не влияет, окно не будет показано в любом случае </summary>
        public bool IsDialogShown { get; set; }

        public EmptyDialogController()
        {
            DialogElement = null;
            IsDialogShown = false;
        }
    }
}