using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;

namespace Web.Models.PageBaseModels
{
    public interface IPageModel
    {
        PageProperties PageProperties { get; }
        string BrowserTitle { get; }
    }

    public abstract class PageModel : IPageModel
    {
        public string BrowserTitle { get; private set; }
        public PageProperties PageProperties { get; private set; }

        protected PageModel(string browserTitle, BunchContextResult bunchContextResult)
        {
            BrowserTitle = browserTitle;
            PageProperties = new PageProperties(bunchContextResult);
        }

        protected PageModel(string browserTitle, AppContextResult appContextResult)
        {
            BrowserTitle = browserTitle;
            PageProperties = new PageProperties(appContextResult);
        }
    }
}