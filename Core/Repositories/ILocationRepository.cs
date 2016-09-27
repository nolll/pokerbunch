using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface ILocationRepository
    {
        Location Get(int id);
        IList<Location> List(string slug);
        int Add(Location location);
    }
}