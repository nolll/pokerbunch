using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel
    {
        public string BrowserTitle { get; private set; }
        public NavigationModel UserNavModel { get; private set; }
        public HomegameNavigationModel HomegameNavModel { get; private set; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }
        public string CssUrl { get; private set; }

        public string Version { get; private set; }

        protected PageModel(string browserTitle, BaseContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            CssUrl = BundleConfig.BundleUrl;
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
        }

        protected PageModel(string browserTitle, AppContextResult contextResult)
            : this(browserTitle, contextResult.Context)
        {
            UserNavModel = new UserNavigationModel(contextResult);
        }

        protected PageModel(string browserTitle, BunchContextResult contextResult)
            : this(browserTitle, contextResult.Context)
        {
            HomegameNavModel = GetHomegameNavModel(contextResult);
        }

        private HomegameNavigationModel GetHomegameNavModel(BunchContextResult bunchContextResult)
        {
            if (bunchContextResult != null && bunchContextResult.HasBunch)
                return new HomegameNavigationModel(bunchContextResult);
            return null;
        }
    }
}