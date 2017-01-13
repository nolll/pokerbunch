using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class EventRepository : IEventRepository
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
            var apiEvent = _api.Get<ApiEvent>($"events/{id}");
            return CreateEvent(apiEvent);
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = _api.Get<IList<ApiEvent>>($"bunches/{bunchId}/events");
            return apiEvents.Select(CreateEvent).ToList();
        }

        public Event GetByCashgame(string cashgameId)
        {
            var ids = _eventDb.FindByBunchId(cashgameId);
            return Get(ids).FirstOrDefault();
        }

        private IList<Event> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_eventDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
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

        private Event CreateEvent(ApiEvent l)
        {
            return new Event(l.Id, l.Bunch, l.Name);
        }

        private class ApiEvent
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Bunch { get; set; }

            public ApiEvent(string name, string bunch)
            {
                Name = name;
                Bunch = bunch;
            }

            public ApiEvent()
            {
            }
        }
    }
}