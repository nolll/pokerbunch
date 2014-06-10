using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameActionChartJsonUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionChartJsonUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameActionChartJson, slug, dateStr, playerId)
        {
        }
    }
}