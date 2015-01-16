using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EventList
{
    public static class EventListInteractor
    {
        public static EventListOutput Execute(IBunchRepository bunchRepository, IEventRepository eventRepository, EventListInput input)
        {
            var bunch = bunchRepository.GetBySlug(input.Slug);
            var events = eventRepository.Find(bunch.Id);

            var eventItems = events.Select(o => CreateEventItem(input.Slug, o)).ToList();

            return new EventListOutput(eventItems);
        }

        private static EventItem CreateEventItem(string slug, Event e)
        {
            var eventDetailsUrl = new EventDetailsUrl(slug, e.Id);

            return new EventItem(e.Name, eventDetailsUrl, e.Location, e.StartDate, e.EndDate);
        }
    }
}