using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly SqlEventDb _eventDb;
        private readonly ICacheContainer _cacheContainer;

        public EventRepository(SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _eventDb = new SqlEventDb(db);
            _cacheContainer = cacheContainer;
        }

        public Event Get(string id)
        {
            return _cacheContainer.GetAndStore(_eventDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Event> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_eventDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> FindByBunchId(string bunchId)
        {
            return _eventDb.FindByBunchId(bunchId);
        }

        public IList<string> FindByCashgameId(string cashgameId)
        {
            return _eventDb.FindByBunchId(cashgameId);
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
    }
}