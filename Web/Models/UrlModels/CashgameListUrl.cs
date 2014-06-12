using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameListUrl : HomegameWithOptionalYearUrl
    {
        public CashgameListUrl(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}