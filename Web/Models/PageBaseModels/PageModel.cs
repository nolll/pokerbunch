using Application.UseCases.AppContext;
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
        }

        protected PageModel(string browserTitle, AppContextResult contextResult)
            : this(browserTitle, (BaseContextResult)contextResult)
        {
            UserNavModel = new UserNavigationModel(contextResult);
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
        }

        protected PageModel(string browserTitle, BunchContextResult contextResult)
            : this(browserTitle, (AppContextResult)contextResult)
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