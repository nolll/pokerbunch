using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlEventRepository : IEventRepository
    {
        private readonly IEventStorage _eventStorage;

        public SqlEventRepository(
            IEventStorage eventStorage)
        {
            _eventStorage = eventStorage;
        }

        public IList<Event> Get(IList<int> ids)
        {
            return FindUncached(ids);
        }

        public IList<int> Find(int bunchId)
        {
            return GetIds(bunchId);
        }

        public Event Get(int id)
        {
            return GetByIdUncached(id);
        }

        private Event GetByIdUncached(int id)
        {
            var rawEvent = _eventStorage.GetById(id);
            return rawEvent != null ? CreateEvent(rawEvent) : null;
        }

        private IList<Event> FindUncached(IList<int> ids)
        {
            var rawEvents = _eventStorage.GetEventList(ids);
            return rawEvents.Select(CreateEvent).ToList();
        }

        private IList<int> GetIds(int bunchId)
        {
            return _eventStorage.GetEventIdList(bunchId);
        }

        private static Event CreateEvent(RawEvent rawEvent)
        {
            return new Event(
                rawEvent.Id,
                rawEvent.BunchId,
                rawEvent.Name,
                rawEvent.Location,
                new Date(rawEvent.StartDate),
                new Date(rawEvent.EndDate));
        }
    }
}