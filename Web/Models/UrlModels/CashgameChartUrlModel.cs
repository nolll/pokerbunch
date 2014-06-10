using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameChartUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}