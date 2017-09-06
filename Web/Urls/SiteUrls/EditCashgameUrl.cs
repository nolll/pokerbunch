using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditCashgameUrl : SiteUrl
    {
        private readonly string _id;

        public EditCashgameUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Edit, RouteReplace.Id(_id));
    }
}