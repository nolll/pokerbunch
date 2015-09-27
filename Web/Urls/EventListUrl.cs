using Web.Common.Routes;

namespace Web.Urls
{
    public class EventListUrl : SlugUrl
    {
        public EventListUrl(string slug)
            : base(WebRoutes.EventList, slug)
        {
        }
    }
}