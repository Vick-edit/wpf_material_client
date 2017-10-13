using System;
using MaterialDesignThemes.Wpf;
using WPF_client.Elements;

namespace WPF_client.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            DemoItems = new DemoItem[] {};
        }

        public DemoItem[] DemoItems { get; }
    }
}