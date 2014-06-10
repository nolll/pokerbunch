using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameActionUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameAction, slug, dateStr, playerId)
        {
        }
    }
}