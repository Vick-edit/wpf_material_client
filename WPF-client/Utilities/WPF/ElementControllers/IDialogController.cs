using System.ComponentModel;
using System.Windows;

namespace WPF_client.Utilities.WPF.ElementControllers
{
    /// <summary> Интерфейс временной реализации всплывающих окон в WPF </summary>
    public interface IDialogController : INotifyPropertyChanged
    {
        /// <summary> WPF, реализующий отображение сообщений </summary>
        FrameworkElement DialogElement { get; }

        /// <summary> Отображается ли сейчас диалоговое коно? </summary>
        bool IsDialogShown { get; set; }
    }
}