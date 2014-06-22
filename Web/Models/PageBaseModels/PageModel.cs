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
        
        protected PageModel(string browserTitle, BunchContextResult contextResult)
            : this(browserTitle, (AppContextResult)contextResult)
        {
            HomegameNavModel = GetHomegameNavModel(contextResult);
        }

        protected PageModel(string browserTitle, AppContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            UserNavModel = new UserNavigationModel(contextResult);
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
            CssUrl = BundleConfig.BundleUrl; 
            Version = contextResult.Version;
        }

        private HomegameNavigationModel GetHomegameNavModel(BunchContextResult bunchContextResult)
        {
            if (bunchContextResult != null && bunchContextResult.HasBunch)
                return new HomegameNavigationModel(bunchContextResult);
            return null;
        }
    }
}