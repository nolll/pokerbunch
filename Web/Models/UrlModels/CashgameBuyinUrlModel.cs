using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameBuyinUrlModel : PlayerUrlModel
    {
        public CashgameBuyinUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameBuyin, slug, playerId)
        {
        }
    }
}