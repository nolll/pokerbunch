using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EventList
{
    public class EventListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IEventRepository _eventRepository;

        public EventListInteractor(IBunchRepository bunchRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _eventRepository = eventRepository;
        }

        public EventListOutput Execute(EventListInput input)
        {
            var bunch = _bunchRepository.GetBySlug(input.Slug);
            var events = _eventRepository.Find(bunch.Id);

            var eventItems = events.OrderByDescending(o => o.StartDate).Select(o => CreateEventItem(input.Slug, o)).ToList();

            return new EventListOutput(eventItems);
        }

        private static EventItem CreateEventItem(string slug, Event e)
        {
            var eventDetailsUrl = new EventDetailsUrl(slug, e.Id);

            return new EventItem(e.Name, eventDetailsUrl, e.Location, e.StartDate, e.EndDate);
        }
    }
}