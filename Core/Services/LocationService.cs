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

        public IList<Location> Get(IList<int> ids)
        {
            return _locationRepository.Get(ids);
        }

        public IList<Location> GetByBunch(int bunchId)
        {
            var ids = _locationRepository.Find(bunchId);
            return _locationRepository.Get(ids);
        }

        public Location GetByName(int bunchId, string name)
        {
            var ids = _locationRepository.Find(bunchId, name);
            return _locationRepository.Get(ids).FirstOrDefault();
        }

        public int Add(Location location)
        {
            return _locationRepository.Add(location);
        }
    }
}