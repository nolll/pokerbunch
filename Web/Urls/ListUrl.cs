using Web.Common.Routes;

namespace Web.Urls
{
    public class ListUrl : BunchWithOptionalYearUrl
    {
        public ListUrl(string slug, int? year)
            : base(WebRoutes.CashgameList, WebRoutes.CashgameListWithYear, slug, year)
        {
        }
    }
}