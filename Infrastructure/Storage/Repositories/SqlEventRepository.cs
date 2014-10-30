using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlEventRepository : IEventRepository
    {
        private readonly IEventStorage _eventStorage;
        private readonly ICacheContainer _cacheContainer;
        private readonly ICacheBuster _cacheBuster;

        public SqlEventRepository(
            IEventStorage eventStorage,
            ICacheContainer cacheContainer,
            ICacheBuster cacheBuster)
        {
            _eventStorage = eventStorage;
            _cacheContainer = cacheContainer;
            _cacheBuster = cacheBuster;
        }

        public IList<Event> Find(int bunchId)
        {
            var ids = GetIds(bunchId);
            var events = _cacheContainer.GetEachAndStore(FindUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return events.OrderBy(o => o.Name).ToList();
        }

        public Event GetById(int id)
        {
            var cacheKey = CacheKeyProvider.EventKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
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
            var cacheKey = CacheKeyProvider.EventIdsKey(bunchId);
            return _cacheContainer.GetAndStore(() => _eventStorage.GetEventIdList(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private static Event CreateEvent(RawEvent rawEvent)
        {
            return new Event(
                rawEvent.Id,
                rawEvent.Name);
        }
    }
}