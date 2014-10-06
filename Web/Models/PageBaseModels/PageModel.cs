using Core.UseCases.BaseContext;
using Web.Models.MiscModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel
    {
        public string BrowserTitle { get; private set; }
        public string CssUrl { get; private set; }
        public string Version { get; private set; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }

        protected PageModel(string browserTitle, BaseContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            CssUrl = BundleConfig.BundleUrl;
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
        }

        public virtual string Layout
        {
            get { return ContextLayout.Base; }
        }
    }
}