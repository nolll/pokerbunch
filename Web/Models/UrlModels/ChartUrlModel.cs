using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ChartUrlModel : HomegameWithOptionalYearUrlModel
    {
        public ChartUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}