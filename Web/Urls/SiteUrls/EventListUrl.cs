using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EventListUrl : SlugUrl
    {
        public EventListUrl(string slug)
            : base(WebRoutes.Event.List, slug)
        {
        }
    }
}