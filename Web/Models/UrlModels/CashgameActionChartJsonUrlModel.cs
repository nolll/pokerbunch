using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameActionChartJsonUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionChartJsonUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameActionChartJson, slug, dateStr, playerId)
        {
        }
    }
}