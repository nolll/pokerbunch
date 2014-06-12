using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ChartUrl : HomegameWithOptionalYearUrl
    {
        public ChartUrl(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}