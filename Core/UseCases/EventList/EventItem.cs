using Core.Urls;

namespace Core.UseCases.EventList
{
    public class EventItem
    {
        public string Name { get; private set; }
        public Url EventDetailsUrl { get; private set; }

        public EventItem(string name, Url eventDetailsUrl)
        {
            Name = name;
            EventDetailsUrl = eventDetailsUrl;
        }
    }
}