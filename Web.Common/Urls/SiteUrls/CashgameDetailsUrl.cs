using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class CashgameDetailsUrl : IdUrl
    {
        public CashgameDetailsUrl(string id)
            : base(WebRoutes.Cashgame.Details, id)
        {
        }
    }
}