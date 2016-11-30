using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface ILocationRepository
    {
        Location Get(string id);
        IList<Location> List(string slug);
        string Add(Location location);
    }
}