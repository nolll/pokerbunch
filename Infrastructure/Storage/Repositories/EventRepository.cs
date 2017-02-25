using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class EventRepository : ApiRepository, IEventRepository
    {
        private readonly SqlEventDb _eventDb;
        private readonly ApiConnection _api;
        private readonly ICacheContainer _cacheContainer;

        public EventRepository(ApiConnection api, SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _eventDb = new SqlEventDb(db);
            _api = api;
            _cacheContainer = cacheContainer;
        }

        public Event Get(string id)
        {
            var apiEvent = _api.Get<ApiEvent>(Url.Event(id));
            return CreateEvent(apiEvent);
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = _api.Get<IList<ApiEvent>>(Url.EventsByBunch(bunchId));
            return apiEvents.Select(CreateEvent).ToList();
        }
        
        public string Add(Event e)
        {
            return _eventDb.Add(e);
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            _eventDb.AddCashgame(eventId, cashgameId);
            _cacheContainer.Remove<Event>(eventId);
        }

        private Event CreateEvent(ApiEvent e)
        {
            var location = new SmallLocation(e.Location.Id, e.Location.Name);
            return new Event(e.Id, e.BunchId, e.Name);
        }

        private class ApiEvent
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string BunchId { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public ApiEventLocation Location { get; set; }

            public ApiEvent(string name, string bunchId)
            {
                Name = name;
                BunchId = bunchId;
            }

            public ApiEvent()
            {
            }
        }

        public class ApiEventLocation
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
        }
    }
}