using System.Collections.Generic;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeLocationService : ILocationService
    {
        public IList<Location> List(string bunchId)
        {
            throw new System.NotImplementedException();
        }
    }
}