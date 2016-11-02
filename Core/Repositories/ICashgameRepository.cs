using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICashgameRepository
    {
        Cashgame Get(string cashgameId);
	    IList<Cashgame> Get(IList<string> ids);

        IList<Cashgame> ListFinished(string bunchId, int? year = null);
        IList<Cashgame> ListByEvent(string eventId);
        IList<Cashgame> ListByPlayer(string playerId);
        Cashgame GetRunning(string bunchId);
        Cashgame FindByCheckpoint(string checkpointId);
        
        void DeleteGame(string id);
        string Add(Bunch bunch, Cashgame cashgame);
		void Update(Cashgame cashgame);

        IList<int> GetYears(string bunchId);
	}
}