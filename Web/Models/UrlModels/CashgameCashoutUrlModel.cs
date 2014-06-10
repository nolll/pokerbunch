using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameCashoutUrlModel : PlayerUrlModel
    {
        public CashgameCashoutUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameCashout, slug, playerId)
        {
        }
    }
}