using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameChartJsonUrl : HomegameWithOptionalYearUrl
    {
        public CashgameChartJsonUrl(string slug, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year)
        {
        }
    }
}