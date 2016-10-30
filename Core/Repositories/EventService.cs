using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            return _eventRepository.ListByBunch(bunchId);
        }

        public Event GetByCashgame(string bunchId)
        {
            return _eventRepository.GetByCashgame(bunchId);
        }

        public Event Get(string eventId)
        {
            return _eventRepository.Get(eventId);
        }

        public string Add(Event e)
        {
            return _eventRepository.Add(e);
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            _eventRepository.AddCashgame(eventId, cashgameId);
        }
    }
}