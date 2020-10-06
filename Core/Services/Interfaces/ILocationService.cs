using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ILocationService
    {
        IList<Location> List(string bunchId);
    }
}