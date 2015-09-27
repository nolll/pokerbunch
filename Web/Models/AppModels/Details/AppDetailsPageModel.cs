using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.Details
{
    public class AppDetailsPageModel : AppPageModel
    {
        public string AppName { get; private set; }
        public string AppKey { get; private set; }
        public bool ShowEditLink { get; private set; }
        public string EditUrl { get; private set; }

        public AppDetailsPageModel(AppContext.Result contextResult, AppDetails.Result appDetailsResult)
            : base("All Details", contextResult)
        {
            AppName = appDetailsResult.AppName;
            AppKey = appDetailsResult.AppKey;
            ShowEditLink = true;
            EditUrl = new EditAppUrl(appDetailsResult.AppId).Relative;
        }
    }
}