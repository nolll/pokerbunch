using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class PlayerDetailsUrl : SiteUrl
    {
        private readonly string _id;

        public PlayerDetailsUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.Details, RouteReplace.Id(_id));
    }
}