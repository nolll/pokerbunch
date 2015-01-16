using Core.Entities;
using Core.Urls;

namespace Core.UseCases.EventList
{
    public class EventItem
    {
        public string Name { get; private set; }
        public Url EventDetailsUrl { get; private set; }
        public string Location { get; private set; }
        public Date StartDate { get; private set; }
        public Date EndDate { get; private set; }

        public EventItem(string name, Url eventDetailsUrl, string location, Date startDate, Date endDate)
        {
            Name = name;
            EventDetailsUrl = eventDetailsUrl;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}