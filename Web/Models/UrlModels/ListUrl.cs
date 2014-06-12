using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ListUrl : HomegameWithOptionalYearUrl
    {
        public ListUrl(string slug, int? year)
            : base(RouteFormats.CashgameList, RouteFormats.CashgameListWithYear, slug, year)
        {
        }
    }
}