using System;
using System.Windows;

namespace WPF_client.Extensions
{
    public static class HandleDisposableViewModelExtension
    {
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