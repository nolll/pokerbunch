using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IEventRepository
    {
        Event Get(int id);
        IList<Event> Get(IList<int> ids);
        IList<int> Find(int bunchId);
        int Add(Event e);
    }
}