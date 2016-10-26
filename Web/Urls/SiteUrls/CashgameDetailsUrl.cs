using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameDetailsUrl : IdUrl
    {
        public CashgameDetailsUrl(string id)
            : base(WebRoutes.Cashgame.Details, id)
        {
        }
    }
}