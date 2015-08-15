using System.Globalization;

namespace Core.Urls
{
    public class EventDetailsUrl : Url
    {
        public EventDetailsUrl(string slug, int eventId)
            : base(Routes.EventDetails.Replace("{slug}", slug).Replace("{eventId}", eventId.ToString(CultureInfo.InvariantCulture)))
        {
        }
    }
}