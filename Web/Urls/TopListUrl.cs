using Web.Common.Routes;

namespace Web.Urls
{
    public class TopListUrl : BunchWithOptionalYearUrl
    {
        public TopListUrl(string slug, int? year)
            : base(WebRoutes.CashgameToplist, WebRoutes.CashgameToplistWithYear, slug, year)
        {
        }
    }
}