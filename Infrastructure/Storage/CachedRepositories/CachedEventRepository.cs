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

        public Event Get(int id)
        {
            return _cacheContainer.GetAndStore(_eventRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Event> Get(IList<int> ids)
        {
            return _cacheContainer.GetAndStore(_eventRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<int> Find(int bunchId)
        {
            return _eventRepository.Find(bunchId);
        }
    }
}