using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IEventRepository
    {
        Event Get(string id);
        IList<Event> ListByBunch(string bunchId);
        string Add(Event e);
        void AddCashgame(string eventId, string cashgameId);
    }
}