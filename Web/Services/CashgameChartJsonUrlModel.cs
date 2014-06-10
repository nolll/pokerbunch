using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameChartJsonUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartJsonUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, slug, year)
        {
        }
    }
}