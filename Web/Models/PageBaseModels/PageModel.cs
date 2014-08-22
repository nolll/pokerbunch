using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;
using Application.UseCases.BunchContext;
using Application.UseCases.CashgameContext;
using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class PageModel
    {
        public string BrowserTitle { get; private set; }
        public string CssUrl { get; private set; }
        public string Version { get; private set; }
        public GoogleAnalyticsModel GoogleAnalyticsModel { get; private set; }
        public NavigationModel UserNavModel { get; private set; }
        public HomegameNavigationModel HomegameNavModel { get; protected set; }

        protected PageModel(string browserTitle, BaseContextResult contextResult)
        {
            BrowserTitle = browserTitle;
            CssUrl = BundleConfig.BundleUrl;
            Version = contextResult.Version;
            GoogleAnalyticsModel = new GoogleAnalyticsModel(contextResult);
            HomegameNavModel = HomegameNavigationModel.Empty;
        }

        protected PageModel(string browserTitle, AppContextResult contextResult)
            : this(browserTitle, contextResult.BaseContext)
        {
            UserNavModel = new UserNavigationModel(contextResult);
        }
    }

    public abstract class AppPageModel : PageModel
    {
        protected AppPageModel(string browserTitle, AppContextResult appContextResult) : base(browserTitle, appContextResult)
        {
        }
    }

    public abstract class BunchPageModel : AppPageModel
    {
        protected BunchPageModel(string browserTitle, BunchContextResult bunchContextResult)
            : base(browserTitle, bunchContextResult.AppContext)
        {
            HomegameNavModel = GetHomegameNavModel(bunchContextResult);
        }

        private HomegameNavigationModel GetHomegameNavModel(BunchContextResult bunchContextResult)
        {
            if (bunchContextResult != null && bunchContextResult.HasBunch)
                return new HomegameNavigationModel(bunchContextResult);
            return null;
        }
    }

    public abstract class CashgamePageModel : BunchPageModel
    {
        protected CashgamePageModel(string browserTitle, CashgameContextResult cashgameContextResult) : base(browserTitle, cashgameContextResult.BunchContext)
        {
        }
    }
}