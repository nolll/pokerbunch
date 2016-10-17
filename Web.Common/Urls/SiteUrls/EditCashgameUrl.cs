using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class EditCashgameUrl : IdUrl
    {
        public EditCashgameUrl(string id)
            : base(WebRoutes.Cashgame.Edit, id)
        {
        }
    }
}