using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICashgameRepository
	{
	    DetailedCashgame GetDetailedById(string id);
	    DetailedCashgame GetCurrent(string bunchId);

        IList<ListCashgame> List(string bunchId, int? year = null);
        IList<ListCashgame> EventList(string eventId);
        IList<ListCashgame> PlayerList(string playerId);
        
        Cashgame GetRunning(string bunchId);
        Cashgame GetByCheckpoint(string checkpointId);
        
        void DeleteGame(string id);
        string Add(Bunch bunch, Cashgame cashgame);
        void Update(Cashgame cashgame);
        DetailedCashgame Update(string id, string locationId, string eventId);

        IList<int> GetYears(string bunchId);

	    void Report(string cashgameId, string playerId, int stack);
	}
}