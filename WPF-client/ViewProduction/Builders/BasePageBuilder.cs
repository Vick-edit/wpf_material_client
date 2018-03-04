using System;
using System.Windows;
using System.Windows.Controls;
using WPF_client.Elements;
using WPF_client.Utilities.WPF.ElementControllers;
using WPF_client.ViewProduction.Interfaces;

namespace WPF_client.ViewProduction.Builders
{
    /// <summary> Общий алгоритм построения любой страницы </summary>
    public class BasePageBuilder : IPageBuilder
    {
        /// <summary> Готовая, построенная страница </summary>
        protected PageContentItem BuildedPage;
        /// <summary> Доступ к диалоговому окну страницы </summary>
        protected IDialogController DialogController;

        /// <summary> Представление (View) страницы </summary>
        protected FrameworkElement ViewElement;
        /// <summary> Контекст представления (VM) страницы </summary>
        protected object ViewModel;

        /// <summary> Имя объекта контекстного меню </summary>
        protected string ContextMenuElementName;
        /// <summary> Список жлементов контекстного меню </summary>
        protected StackPanel ContextMenuItems;


        /// <summary> Построить по заданным параметрам страницу приложения </summary>
        /// <param name="pageName">Имя страницы для меню</param>
        /// <returns>Готовая страница</returns>
        public virtual PageContentItem GetNewPage(string pageName)
        {
            if(ViewElement == null)
                throw new NullReferenceException("Не задан графический элемент страницы");
            var dialogController = DialogController ?? new EmptyDialogController();

            return new PageContentItem(pageName, ViewElement, dialogController, ContextMenuItems);
        }


        /// <summary> Задать бизнеслогику страницы </summary>
        public virtual void SetupBuisnesLogic()
        {
        }

        /// <summary> Построить VM для данной страницы </summary>
        public virtual void SetupViewModel()
        {
        }

        /// <summary> Построить view данной страницы </summary>
        public virtual void SetupView()
        {
            if (ViewModel == null)
                throw new NullReferenceException("Не задан контекст графического элемента страницы");
        }

        /// <summary> Построить контекстное меню для данной страницы </summary>
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