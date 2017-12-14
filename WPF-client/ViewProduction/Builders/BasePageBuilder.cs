using System;
using System.Windows;
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


        public virtual PageContentItem GetNewPage(string pageName)
        {
            if(ViewElement == null)
                throw new NullReferenceException("Не задан графический элемент страницы");
            var dialogController = DialogController ?? new EmptyDialogController();

            return new PageContentItem(pageName, ViewElement, dialogController);
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
    }
}