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

        public IList<Event> Find(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public Event GetById(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}