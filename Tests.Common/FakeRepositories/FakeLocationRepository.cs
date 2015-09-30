using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeLocationRepository : ILocationRepository
    {
        private readonly IList<Location> _list;

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
            return _list.Where(o => o.BunchId == bunchId).Select(o => o.Id).ToList();
        }
        
        private IList<Location> CreateLocationList()
        {
            return new[]
            {
                new Location(TestData.LocationIdA, TestData.LocationNameA, TestData.BunchA.Id),
                new Location(TestData.LocationIdB, TestData.LocationNameB, TestData.BunchA.Id)
            };
        }
    }
}