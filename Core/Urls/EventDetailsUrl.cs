namespace Core.Urls
{
    public class EventDetailsUrl : Url
    {
        public EventDetailsUrl(string slug, int eventId)
            : base(BuildUrl(slug, eventId))
        {
        }

        private static string BuildUrl(string slug, int eventId)
        {
            var url = RouteParams.ReplaceSlug(Routes.EventDetails, slug);
            return RouteParams.ReplaceEventId(url, eventId);
        }
    }
}