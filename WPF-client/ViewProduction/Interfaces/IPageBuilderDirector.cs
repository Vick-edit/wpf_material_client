using WPF_client.Elements;

namespace WPF_client.ViewProduction
{
    public interface IPageBuilderDirector
    {
        PageContentItem GetPageContentItem(string pageTitle, IPageBuilder pageBuilder);
    }
}