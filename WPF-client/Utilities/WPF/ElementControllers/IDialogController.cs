using System.ComponentModel;
using System.Windows;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    public interface IDialogController : INotifyPropertyChanged
    {
        FrameworkElement DialogElement { get; }

        bool IsDialogShown { get; set; }
    }
}