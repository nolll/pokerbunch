using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IEventService
    {
        Event Get(string id);
        IList<Event> ListByBunch(string bunchId);
        string Add(Event e);
        void AddCashgame(string eventId, string cashgameId);
    }
}