using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddPlayerUrl : SiteUrl
    {
        private readonly string _slug;

        public AddPlayerUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.Add, RouteReplace.Slug(_slug));
    }
}