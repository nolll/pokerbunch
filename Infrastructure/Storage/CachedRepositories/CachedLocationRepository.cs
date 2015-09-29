using System.Collections.Generic;
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

        public IList<string> GetLocations(int bunchId)
        {
            return _locationRepository.GetLocations(bunchId);
        }
    }
}