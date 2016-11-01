using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICashgameService
    {
        IList<Cashgame> ListFinished(string bunchId, int? year = null);
        IList<Cashgame> ListByEvent(string eventId);
        Cashgame GetRunning(string bunchId);
        Cashgame GetByCheckpoint(string checkpointId);
        Cashgame GetById(string cashgameId);
        IList<int> GetYears(string bunchId);
        void DeleteGame(string id);
        string Add(Bunch bunch, Cashgame cashgame);
        void Update(Cashgame cashgame);
        void End(Cashgame cashgame);
        bool HasPlayed(string playerId);
    }
}