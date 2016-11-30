using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.ApiUrls;
using Web.Urls.SiteUrls;

namespace Web.Models.AppModels.Details
{
    public class ApiDocsPageModel : AppPageModel
    {
        public string AppListUrl { get; private set; }
        public string TokenUrl { get; private set; }
        public string BunchListUrl { get; private set; }
        public string RunningGameUrl { get; private set; }
        public string BuyinUrl { get; private set; }
        public string ReportUrl { get; private set; }
        public string CashoutUrl { get; private set; }

        public ApiDocsPageModel(CoreContext.Result contextResult)
            : base(contextResult)
        {
            const string slug = "bunch-short-name";
            AppListUrl = new UserAppsUrl().Relative;
            TokenUrl = new TokenUrl().GetAbsolute();
            BunchListUrl = new ApiBunchListUrl().GetAbsolute();
            RunningGameUrl = new ApiRunningGameUrl(slug).GetAbsolute();
            BuyinUrl = new ApiBuyinUrl(slug).GetAbsolute();
            ReportUrl = new ApiReportUrl(slug).GetAbsolute();
            CashoutUrl = new ApiCashoutUrl(slug).GetAbsolute();
        }

        public override string BrowserTitle => "Api Documentation";
    }
}