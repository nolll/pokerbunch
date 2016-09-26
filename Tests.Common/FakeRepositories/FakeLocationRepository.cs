using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeLocationRepository : ILocationRepository
    {
        private readonly IList<Location> _list;
        public Location Added { get; private set; }
        
        public FakeLocationRepository()
        {
            _list = CreateLocationList();
        }

        public Location Get(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public IList<Location> Get(IList<int> ids)
        {
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public IList<int> Find(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> Find(int bunchId, string name)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> Find(string slug)
        {
            return _list.Where(o => o.Slug == slug).Select(o => o.Id).ToList();
        }

        public IList<int> Find(string slug, string name)
        {
            return _list.Where(o => o.Slug == slug && o.Name == name).Select(o => o.Id).ToList();
        }

        public int Add(Location location)
        {
            Added = location;
            const int id = 1000;
            _list.Add(new Location(id, location.Name, location.Slug));
            return id;
        }

        private IList<Location> CreateLocationList()
        {
            return new List<Location>
            {
                new Location(TestData.LocationIdA, TestData.LocationNameA, TestData.BunchA.Slug),
                new Location(TestData.LocationIdB, TestData.LocationNameB, TestData.BunchA.Slug),
                new Location(TestData.LocationIdC, TestData.LocationNameC, TestData.BunchA.Slug),
                new Location(TestData.ChangedLocationId, TestData.ChangedLocationName, TestData.BunchA.Slug)
            };
        }
    }
}