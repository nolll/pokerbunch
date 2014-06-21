using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel : IPageModel
    {
        public string BrowserTitle { get; private set; }
        public PageProperties PageProperties { get; private set; }

        protected PageModel(string browserTitle, BunchContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            PageProperties = new PageProperties(contextResult);
        }

        protected PageModel(string browserTitle, AppContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            PageProperties = new PageProperties(contextResult);
        }
    }
}