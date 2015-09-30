using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public IList<Location> GetLocations(int bunchId)
        {
            var ids = _locationRepository.Find(bunchId);
            return _locationRepository.Get(ids);
        }
    }
}