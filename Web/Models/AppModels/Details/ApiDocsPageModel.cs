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
            TokenUrl = new TokenUrl().Absolute;
            BunchListUrl = new ApiBunchListUrl().Absolute;
            RunningGameUrl = new ApiRunningGameUrl(slug).Absolute;
            BuyinUrl = new ApiBuyinUrl(slug).Absolute;
            ReportUrl = new ApiReportUrl(slug).Absolute;
            CashoutUrl = new ApiCashoutUrl(slug).Absolute;
        }

        public override string BrowserTitle => "Api Documentation";

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/ApiDocs.cshtml");
        }
    }
}