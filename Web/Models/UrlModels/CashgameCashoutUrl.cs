using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameCashoutUrl : PlayerUrl
    {
        public CashgameCashoutUrl(string slug, int playerId)
            : base(RouteFormats.CashgameCashout, slug, playerId)
        {
        }
    }
}