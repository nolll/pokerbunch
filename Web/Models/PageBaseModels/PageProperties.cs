using Application.UseCases.AppContext;
using Application.UseCases.BunchContext;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public sealed class PageProperties
    {
        public NavigationModel UserNavModel { get; private set; }
        public HomegameNavigationModel HomegameNavModel { get; private set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }
        public string Version { get; private set; }
        public string CssUrl { get; private set; }

        public PageProperties(AppContextResult appContextResult)
        {
            UserNavModel = new UserNavigationModel(appContextResult);
            GoogleAnalyticsModel = new GoogleAnalyticsModel(appContextResult);
            Version = appContextResult.Version;
            CssUrl = BundleConfig.BundleUrl;
        }

        public PageProperties(BunchContextResult bunchContextResult) : this((AppContextResult)bunchContextResult)
        {
            HomegameNavModel = GetHomegameNavModel(bunchContextResult);
        }

        private HomegameNavigationModel GetHomegameNavModel(BunchContextResult bunchContextResult)
        {
            if(bunchContextResult != null && bunchContextResult.HasBunch)
                return new HomegameNavigationModel(bunchContextResult);
            return null;
        }
    }
}