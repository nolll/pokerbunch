using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class PlayerIndexUrl : SiteUrl
    {
        private readonly string _slug;

        public PlayerIndexUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.List, RouteParam.Slug(_slug));
    }
}