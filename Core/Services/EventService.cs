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
            return _eventRepository.Find(bunchId);
        }

        public Event GetById(int eventId)
        {
            return _eventRepository.GetById(eventId);
        }
    }
}