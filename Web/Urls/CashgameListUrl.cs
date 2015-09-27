using Web.Common.Routes;

namespace Web.Urls
{
    public class CashgameListUrl : BunchWithOptionalYearUrl
    {
        public CashgameListUrl(string slug, int? year)
            : base(WebRoutes.CashgameList, WebRoutes.CashgameListWithYear, slug, year)
        {
        }
    }
}