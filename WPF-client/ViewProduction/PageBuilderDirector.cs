using WPF_client.Elements;
using WPF_client.ViewProduction.Interfaces;

namespace WPF_client.ViewProduction
{
    /// <summary> Директор, отвечающий за построение форм приложения </summary>
    public class PageBuilderDirector : IPageBuilderDirector
    {
        /// <summary> Построить форму приложения </summary>
        /// <param name="pageTitle">Название формы в меню</param>
        /// <param name="pageBuilder">Строитель для данной страницы</param>
        /// <returns>Готовая страница приложения</returns>
        public PageContentItem GetPageContentItem(string pageTitle, IPageBuilder pageBuilder)
        {
            pageBuilder.SetupBuisnesLogic();
            pageBuilder.SetupViewModel();
            pageBuilder.SetupView();
            pageBuilder.SetupContextMenu();
            return pageBuilder.GetNewPage(pageTitle);
        }
    }
}