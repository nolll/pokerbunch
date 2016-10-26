using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameReportUrl : SlugUrl
    {
        public CashgameReportUrl(string slug)
            : base(WebRoutes.Cashgame.Report, slug)
        {
        }
    }
}