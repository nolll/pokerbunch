namespace Core.Urls
{
    public class EventDetailsUrl : Url
    {
        public EventDetailsUrl(string slug, int id)
            : base(string.Format(RouteFormats.EventDetails, slug, id))
        {
        }
    }
}