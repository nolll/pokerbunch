using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ILocationService
    {
        Location Get(string id);
        IList<Location> List(string bunchId);
        string Add(Location location);
    }
}