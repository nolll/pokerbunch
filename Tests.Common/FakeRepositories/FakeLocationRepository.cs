using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeLocationRepository : ILocationRepository
    {
        private readonly IList<Location> _list;
        public Location Added { get; private set; }
        public bool ThrowOnAdd { get; set; }
        
        public FakeLocationRepository()
        {
            _list = CreateLocationList();
        }

        public Location Get(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public IList<Location> List(string slug)
        {
            return _list.Where(o => o.BunchId == slug).ToList();
        }

        public string Add(Location location)
        {
            if (ThrowOnAdd)
                throw new ValidationException("validation exception");
            Added = location;
            const string id = "1000";
            _list.Add(new Location(id, location.Name, location.BunchId));
            return id;
        }

        private IList<Location> CreateLocationList()
        {
            return new List<Location>
            {
                new Location(TestData.LocationIdA, TestData.LocationNameA, TestData.BunchA.Id),
                new Location(TestData.LocationIdB, TestData.LocationNameB, TestData.BunchA.Id),
                new Location(TestData.LocationIdC, TestData.LocationNameC, TestData.BunchA.Id),
                new Location(TestData.ChangedLocationId, TestData.ChangedLocationName, TestData.BunchA.Id)
            };
        }
    }
}