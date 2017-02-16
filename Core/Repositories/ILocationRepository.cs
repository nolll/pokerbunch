using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface ILocationRepository
    {
        Location Get(string id);
        IList<Location> List(string id);
        string Add(Location location);
    }
}