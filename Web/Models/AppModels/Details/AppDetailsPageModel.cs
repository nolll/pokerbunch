using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.AppModels.Details
{
    public class AppDetailsPageModel : AppPageModel
    {
        public string AppName { get; private set; }
        public string AppKey { get; private set; }
        public bool ShowEditLink { get; private set; }
        public string EditUrl { get; private set; }

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