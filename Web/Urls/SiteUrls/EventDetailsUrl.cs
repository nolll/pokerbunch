namespace Web.Urls.SiteUrls
{
    public class EventDetailsUrl : IdUrl
    {
        public EventDetailsUrl(string eventId)
            : base(WebRoutes.Event.Details, eventId)
        {
        }
    }
}