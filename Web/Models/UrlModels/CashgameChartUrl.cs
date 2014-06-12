using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameChartUrl : HomegameWithOptionalYearUrl
    {
        public CashgameChartUrl(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}