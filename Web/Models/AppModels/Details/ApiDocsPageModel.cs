using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.AppModels.Details
{
    public class ApiDocsPageModel : AppPageModel
    {
        public string AppListUrl { get; private set; }
        public string TokenUrl { get; private set; }
        public string RunningGameUrl { get; private set; }

        public ApiDocsPageModel(AppContext.Result contextResult)
            : base("All Details", contextResult)
        {
            AppListUrl = new UserAppsUrl().Relative;
            TokenUrl = new TokenUrl().Absolute;
            RunningGameUrl = new ApiRunningGameUrl("bunch-short-name").Absolute;
        }
    }
}