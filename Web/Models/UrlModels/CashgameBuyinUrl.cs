using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameBuyinUrl : PlayerUrl
    {
        public CashgameBuyinUrl(string slug, int playerId)
            : base(RouteFormats.CashgameBuyin, slug, playerId)
        {
        }
    }
}