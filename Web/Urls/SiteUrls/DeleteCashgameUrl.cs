using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DeleteCashgameUrl : SiteUrl
    {
        private readonly string _id;

        public DeleteCashgameUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Delete, RouteReplace.Id(_id));
    }
}