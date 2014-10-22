using System.Collections.Generic;

namespace Core.UseCases.EventList
{
    public class EventListOutput
    {
        public IList<EventItem> Events { get; private set; }

        public EventListOutput(IList<EventItem> events)
        {
            Events = events;
        }
    }
}