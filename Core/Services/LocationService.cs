using System.Collections.Generic;
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

        public IList<string> GetLocations(int bunchId)
        {
            return _locationRepository.GetLocations(bunchId);
        }
    }
}