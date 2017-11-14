using System;
using MaterialDesignThemes.Wpf;
using WPF_client.Elements;
using WPF_client.View;

namespace WPF_client.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            MainMenuItems = new PageContentItem[]
            {
                new PageContentItem("График", new MainChart { DataContext = new MainChartViewModel() }), 
                new PageContentItem("Цветовая тема", new PaletteSelector { DataContext = new PaletteSelectorViewModel() }),
            };
        }

        public PageContentItem[] MainMenuItems { get; }
    }
}