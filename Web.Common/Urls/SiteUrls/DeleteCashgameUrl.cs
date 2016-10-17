using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class DeleteCashgameUrl : IdUrl
    {
        public DeleteCashgameUrl(string id)
            : base(WebRoutes.Cashgame.Delete, id)
        {
        }
    }
}