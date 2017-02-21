using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICashgameRepository
	{
	    DetailedCashgame GetDetailedById(string id);

        CashgameCollection List(string bunchId, int? year = null);
        CashgameCollection EventList(string eventId);
        CashgameCollection PlayerList(string playerId);
        IList<Cashgame> ListFinished(string bunchId, int? year = null);
        Cashgame GetRunning(string bunchId);
        Cashgame GetByCheckpoint(string checkpointId);
        
        void DeleteGame(string id);
        string Add(Bunch bunch, Cashgame cashgame);
        void Update(Cashgame cashgame);
        DetailedCashgame Update(string id, string locationId, string eventId);

        IList<int> GetYears(string bunchId);
	}
}