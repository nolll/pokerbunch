using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICashgameRepository
	{
	    DetailedCashgame GetDetailedById(string id);
        Cashgame GetById(string cashgameId);

        IList<Cashgame> ListFinished(string bunchId, int? year = null);
        IList<Cashgame> ListByEvent(string eventId);
        IList<Cashgame> ListByPlayer(string playerId);
        Cashgame GetRunning(string bunchId);
        Cashgame GetByCheckpoint(string checkpointId);
        
        void DeleteGame(string id);
        string Add(Bunch bunch, Cashgame cashgame);
        void Update(Cashgame cashgame);
        DetailedCashgame Update(string id, string locationId, string eventId);

        IList<int> GetYears(string bunchId);
	}
}