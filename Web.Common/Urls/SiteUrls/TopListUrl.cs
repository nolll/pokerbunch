using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class TopListUrl : BunchWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(WebRoutes.CashgameToplist, WebRoutes.CashgameToplistWithYear, slug, year)
        {
        }
    }
}