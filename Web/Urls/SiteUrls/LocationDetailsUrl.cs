using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LocationDetailsUrl : IdUrl
    {
        public LocationDetailsUrl(string locationId)
            : base(WebRoutes.Location.Details, locationId)
        {
        }
    }
}