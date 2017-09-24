using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.Details
{
    public class AppDetailsPageModel : AppPageModel
    {
        public string AppName { get; }
        public string AppKey { get; }
        public bool ShowEditLink { get; }
        public string EditUrl { get; }

        public AppDetailsPageModel(CoreContext.Result contextResult, AppDetails.Result appDetailsResult)
            : base(contextResult)
        {
            AppName = appDetailsResult.AppName;
            AppKey = appDetailsResult.AppKey;
            ShowEditLink = true;
            EditUrl = new EditAppUrl(appDetailsResult.AppId).Relative;
        }

        public override string BrowserTitle => "All Details";

        public override View GetView()
        {
            return new View("~/Views/Pages/AppDetails/AppDetails.cshtml");
        }
    }
}