using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameBuyinUrlModel : PlayerUrlModel
    {
        public CashgameBuyinUrlModel(string slug, int playerId)
            : base(RouteFormats.CashgameBuyin, slug, playerId)
        {
        }
    }
}