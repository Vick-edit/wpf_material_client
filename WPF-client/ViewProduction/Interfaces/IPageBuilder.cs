using WPF_client.Elements;

namespace WPF_client.ViewProduction.Interfaces
{
    /// <summary> Интерфейс строителя страниц, определяет общий алгоритм построения страниц </summary>
    public interface IPageBuilder
    {
        /// <summary> Построить по заданным параметрам страницу приложения </summary>
        /// <param name="pageName">Имя страницы для меню</param>
        /// <returns>Готовая страница</returns>
        PageContentItem GetNewPage(string pageName);

        /// <summary> Задать бизнеслогику страницы </summary>
        void SetupBuisnesLogic();

        /// <summary> Построить VM для данной страницы </summary>
        void SetupViewModel();

        /// <summary> Построить view данной страницы </summary>
        void SetupView();

        /// <summary> Построить контекстное меню для данной страницы </summary>
        void SetupContextMenu();
    }
}