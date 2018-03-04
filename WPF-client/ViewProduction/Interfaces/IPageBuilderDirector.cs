using WPF_client.Elements;

namespace WPF_client.ViewProduction.Interfaces
{
    /// <summary> Интефейс директора, строящего формы приложения </summary>
    public interface IPageBuilderDirector
    {
        /// <summary> Построить форму приложения </summary>
        /// <param name="pageTitle">Название формы в меню</param>
        /// <param name="pageBuilder">Строитель для данной страницы</param>
        /// <returns>Готовая страница приложения</returns>
        PageContentItem GetPageContentItem(string pageTitle, IPageBuilder pageBuilder);
    }
}