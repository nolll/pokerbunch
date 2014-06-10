using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameCashoutUrlModel : PlayerUrlModel
    {
        public CashgameCashoutUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameCashout, slug, playerId)
        {
        }
    }
}