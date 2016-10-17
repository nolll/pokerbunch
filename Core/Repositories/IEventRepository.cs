using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
    public interface IEventRepository
    {
        Event Get(string id);
        IList<Event> Get(IList<string> ids);
        IList<string> FindByBunchId(string bunchId);
        IList<string> FindByCashgameId(string cashgameId);
        string Add(Event e);
        void AddCashgame(string eventId, string cashgameId);
    }
}