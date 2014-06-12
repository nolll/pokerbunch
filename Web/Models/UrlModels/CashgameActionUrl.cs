using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameActionUrl : CashgamePlayerUrl
    {
        public CashgameActionUrl(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameAction, slug, dateStr, playerId)
        {
        }
    }
}