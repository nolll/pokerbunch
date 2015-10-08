using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class LocationListUrl : SlugUrl
    {
        public LocationListUrl(string slug)
            : base(WebRoutes.LocationList, slug)
        {
        }
    }
}