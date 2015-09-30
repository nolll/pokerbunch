using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedLocationRepository : ILocationRepository
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedLocationRepository(ILocationRepository locationRepository, ICacheContainer cacheContainer)
        {
            _locationRepository = locationRepository;
            _cacheContainer = cacheContainer;
        }

        public Location Get(int id)
        {
            return _cacheContainer.GetAndStore(_locationRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Location> Get(IList<int> ids)
        {
            return _cacheContainer.GetAndStore(_locationRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<int> Find(int bunchId)
        {
            return _locationRepository.Find(bunchId);
        }
    }
}