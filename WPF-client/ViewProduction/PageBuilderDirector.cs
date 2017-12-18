using WPF_client.Elements;

namespace WPF_client.ViewProduction
{
    public class PageBuilderDirector : IPageBuilderDirector
    {
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