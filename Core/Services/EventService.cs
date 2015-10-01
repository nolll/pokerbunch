using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IList<Event> Find(int bunchId)
        {
            var ids = _eventRepository.Find(bunchId);
            return _eventRepository.Get(ids);
        }

        public Event Get(int eventId)
        {
            return _eventRepository.Get(eventId);
        }

        public int Add(Event e)
        {
            return _eventRepository.Add(e);
        }
    }
}