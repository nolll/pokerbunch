using Application.UseCases.CashgameContext;
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

        public PageProperties()
        {
        }

        public PageProperties(
            BunchContextResult bunchContextResult)
        {
            UserNavModel = new UserNavigationModel(bunchContextResult);
            HomegameNavModel = bunchContextResult != null ? new HomegameNavigationModel(bunchContextResult) : null;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(bunchContextResult);
            Version = bunchContextResult.Version;
            CssUrl = BundleConfig.BundleUrl;
        }

        public PageProperties(
            NavigationModel userNavModel,
            GoogleAnalyticsModel googleAnalyticsModel,
            HomegameNavigationModel homegameNavModel,
            string version,
            string cssUrl)
        {
            UserNavModel = userNavModel;
            HomegameNavModel = homegameNavModel;
            GoogleAnalyticsModel = googleAnalyticsModel;
            Version = version;
            CssUrl = cssUrl;
        }
    }
}