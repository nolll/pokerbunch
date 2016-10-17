using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedEventRepository : IEventRepository
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedEventRepository(IEventRepository eventRepository, ICacheContainer cacheContainer)
        {
            _eventRepository = eventRepository;
            _cacheContainer = cacheContainer;
        }

        public Event Get(string id)
        {
            return _cacheContainer.GetAndStore(_eventRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Event> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_eventRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> FindByBunchId(string bunchId)
        {
            return _eventRepository.FindByBunchId(bunchId);
        }

        public IList<string> FindByCashgameId(string cashgameId)
        {
            return _eventRepository.FindByBunchId(cashgameId);
        }

        public string Add(Event e)
        {
            return _eventRepository.Add(e);
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            _eventRepository.AddCashgame(eventId, cashgameId);
            _cacheContainer.Remove<Event>(eventId);
        }
    }
}