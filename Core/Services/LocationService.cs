using System.Collections.Generic;
using System.Linq;
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

        public Location Get(int id)
        {
            return _locationRepository.Get(id);
        }

        public IList<Location> List(string slug)
        {
            return _locationRepository.List(slug);
        }

        public int Add(Location location)
        {
            return _locationRepository.Add(location);
        }
    }
}