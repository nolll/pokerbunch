using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameChartUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameChartUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, slug, year)
        {
        }
    }
}