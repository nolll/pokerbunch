using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameChartJsonUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartJsonUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year)
        {
        }
    }
}