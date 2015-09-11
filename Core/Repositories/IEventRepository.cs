using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IEventRepository
    {
        IList<Event> Find(int bunchId);
        Event Get(int eventId);
    }
}