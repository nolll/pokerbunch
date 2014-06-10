using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameActionUrlModel : CashgamePlayerUrlModel
    {
        public CashgameActionUrlModel(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameAction, slug, dateStr, playerId)
        {
        }
    }
}