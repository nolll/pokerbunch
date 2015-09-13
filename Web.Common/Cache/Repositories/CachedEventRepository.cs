using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
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

        public IList<Event> Get(IList<int> ids)
        {
            return _eventRepository.Get(ids);
        }

        public IList<int> Find(int bunchId)
        {
            return _eventRepository.Find(bunchId);
        }

        public Event Get(int id)
        {
            return _eventRepository.Get(id);
        }
    }
}