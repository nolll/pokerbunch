using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameDetailsChartJsonUrlModel : CashgameUrlModel
    {
        public CashgameDetailsChartJsonUrlModel(string slug, string dateStr)
            : base(RouteFormats.CashgameDetailsChartJson, slug, dateStr)
        {
        }
    }
}