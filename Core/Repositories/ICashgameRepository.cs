using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories
{
	public interface ICashgameRepository
    {
        Cashgame Get(string cashgameId);
	    IList<Cashgame> Get(IList<string> ids);

        IList<string> FindFinished(string bunchId, int? year = null);
        IList<string> FindByEvent(string eventId);
        IList<string> FindByPlayerId(string playerId);
        IList<string> FindRunning(string bunchId);
        IList<string> FindByCheckpoint(string checkpointId);
        
        void DeleteGame(string id);
        string AddGame(Bunch bunch, Cashgame cashgame);
		void UpdateGame(Cashgame cashgame);

        IList<int> GetYears(string bunchId);
	}
}