using Web.Common.Routes;

namespace Web.Urls
{
    public class EventDetailsUrl : IdUrl
    {
        public EventDetailsUrl(int eventId)
            : base(WebRoutes.EventDetails, eventId)
        {
        }
    }
}