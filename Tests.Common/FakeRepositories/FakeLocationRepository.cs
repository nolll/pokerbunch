using System.Collections.Generic;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeLocationRepository : ILocationRepository
    {
        public IList<string> GetLocations(int bunchId)
        {
            return new[] { TestData.LocationA, TestData.LocationB };
        }
    }
}