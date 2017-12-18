using WPF_client.Elements;

namespace WPF_client.ViewProduction
{
    public interface IPageBuilder
    {
        PageContentItem GetNewPage(string pageName);

        void SetupBuisnesLogic();

        void SetupViewModel();

        void SetupView();

        void SetupContextMenu();
    }
}