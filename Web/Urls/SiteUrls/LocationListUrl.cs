using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LocationListUrl : SiteUrl
    {
        private readonly string _slug;

        public LocationListUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Location.List, RouteReplace.Slug(_slug));
    }
}