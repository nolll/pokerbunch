using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameReportUrl : SiteUrl
    {
        private readonly string _slug;

        public CashgameReportUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Report, RouteReplace.Slug(_slug));
    }
}