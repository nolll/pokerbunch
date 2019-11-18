using Core.Settings;
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
        public string DeleteUrl { get; }

        public AppDetailsPageModel(AppSettings appSettings, CoreContext.Result contextResult, AppDetails.Result appDetailsResult)
            : base(appSettings, contextResult)
        {
            AppName = appDetailsResult.AppName;
            AppKey = appDetailsResult.AppKey;
            DeleteUrl = new DeleteAppUrl(appDetailsResult.AppId).Relative;
        }

        public override string BrowserTitle => "All Details";

        public override View GetView()
        {
            return new View("~/Views/Pages/AppDetails/AppDetails.cshtml");
        }
    }
}