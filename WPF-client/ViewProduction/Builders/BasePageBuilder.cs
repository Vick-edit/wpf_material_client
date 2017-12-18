using System;
using System.Windows;
using System.Windows.Controls;
using WPF_client.Elements;
using WPF_client.Utilities.WPF.ElementControllers;

namespace WPF_client.ViewProduction.Builders
{
    public class BasePageBuilder : IPageBuilder
    {
        protected PageContentItem BuildedPage;
        protected IDialogController DialogController;

        protected FrameworkElement ViewElement;
        protected object ViewModel;

        protected string ContextMenuElementName;
        protected StackPanel ContextMenuItems;

        public virtual PageContentItem GetNewPage(string pageName)
        {
            if(ViewElement == null)
                throw new NullReferenceException("Не задан графический элемент страницы");
            var dialogController = DialogController ?? new EmptyDialogController();

            return new PageContentItem(pageName, ViewElement, dialogController, ContextMenuItems);
        }


        public virtual void SetupBuisnesLogic()
        {
        }

        public virtual void SetupViewModel()
        {
        }

        public virtual void SetupView()
        {
            if (ViewModel == null)
                throw new NullReferenceException("Не задан контекст графического элемента страницы");
        }

        public void SetupContextMenu()
        {
            if (ViewElement == null)
                throw new NullReferenceException("Не задан графический элемент страницы");
            if (ViewModel == null)
                throw new NullReferenceException("Не задан контекст графического элемента страницы");
            if (string.IsNullOrEmpty(ContextMenuElementName))
            {
                ContextMenuItems = null;
                return;
            }


            var hiddenContextMenu = ViewElement.FindName(ContextMenuElementName) as HiddenContextMenu;
            if ( !(hiddenContextMenu?.PopupContent is StackPanel) )
            {
                ContextMenuItems = null;
                return;
            }

            ContextMenuItems = (StackPanel) hiddenContextMenu.PopupContent;
            hiddenContextMenu.PopupContent = null;
            ContextMenuItems.DataContext = ViewModel;
        }
    }
}