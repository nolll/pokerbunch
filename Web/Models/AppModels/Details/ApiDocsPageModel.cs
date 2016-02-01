using Core.UseCases;
using Web.Common.Urls.ApiUrls;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

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
            : base("Api Documentation", contextResult)
        {
            const string slug = "bunch-short-name";
            AppListUrl = new UserAppsUrl().Relative;
            TokenUrl = new TokenUrl().Absolute;
            BunchListUrl = new ApiBunchListUrl().Absolute;
            RunningGameUrl = new ApiRunningGameUrl(slug).Absolute;
            BuyinUrl = new ApiBuyinUrl(slug).Absolute;
            ReportUrl = new ApiReportUrl(slug).Absolute;
            CashoutUrl = new ApiCashoutUrl(slug).Absolute;
        }
    }
}