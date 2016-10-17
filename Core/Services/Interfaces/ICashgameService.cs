using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICashgameService
    {
        IList<Cashgame> GetFinished(string bunchId, int? year = null);
        IList<Cashgame> GetByEvent(string eventId);
        Cashgame GetRunning(string bunchId);
        Cashgame GetByCheckpoint(string checkpointId);
        Cashgame GetById(string cashgameId);
        IList<int> GetYears(string bunchId);
        void DeleteGame(string id);
        string AddGame(Bunch bunch, Cashgame cashgame);
        void UpdateGame(Cashgame cashgame);
        void EndGame(Cashgame cashgame);
        bool HasPlayed(string playerId);
    }
}