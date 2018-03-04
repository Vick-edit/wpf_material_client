using System;
using System.Windows;

namespace WPF_client.Extensions
{
    /// <summary> Расширение для форм, у которых DataContext содержат Disposable элементы </summary>
    public static class HandleDisposableViewModelExtension
    {
        /// <summary> Прикрепить высвобождение неуправляемых элементов к закрытию элемента </summary>
        /// <param name="Element">View для которого будет происходить высвобождение неуправляемых ресурсов</param>
        public static void HandleDisposableViewModel(this FrameworkElement Element)
        {
            Action Dispose = () =>
            {
                var DataContext = Element.DataContext as IDisposable;
                DataContext?.Dispose();
            };

            Element.Dispatcher.ShutdownStarted += (s, ea) => Dispose();
        }
    }
}