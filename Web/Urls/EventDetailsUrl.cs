using Core.Urls;

namespace Web.Urls
{
    public class EventDetailsUrl : IdUrl
    {
        public EventDetailsUrl(int eventId)
            : base(Routes.EventDetails, eventId)
        {
        }
    }
}