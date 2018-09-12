using System.Collections.Generic;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeLocationService : ILocationService
    {
        public Location Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Location> List(string bunchId)
        {
            throw new System.NotImplementedException();
        }

        public string Add(Location location)
        {
            throw new System.NotImplementedException();
        }
    }
}