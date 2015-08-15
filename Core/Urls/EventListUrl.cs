namespace Core.Urls
{
    public class EventListUrl : BunchUrl
    {
        public EventListUrl(string slug)
            : base(Routes.EventList, slug)
        {
        }
    }
}