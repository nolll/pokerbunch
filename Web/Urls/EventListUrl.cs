namespace Web.Urls
{
    public class EventListUrl : SlugUrl
    {
        public EventListUrl(string slug)
            : base(Routes.EventList, slug)
        {
        }
    }
}