using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Core.Repositories
{
	public interface ICashgameRepository
    {
        IList<Cashgame> GetFinished(int bunchId, int? year = null);
        IList<Cashgame> GetByEvent(int eventId);
        Cashgame GetRunning(int bunchId);
        Cashgame GetById(int cashgameId);
        IList<int> GetYears(int bunchId);
		IList<string> GetLocations(int bunchId);
		bool DeleteGame(Cashgame cashgame);
		int AddGame(Bunch bunch, Cashgame cashgame);
		bool UpdateGame(Cashgame cashgame);
        bool EndGame(Bunch bunch, Cashgame cashgame);
		bool HasPlayed(int playerId);

        int AddCheckpoint(Checkpoint checkpoint);
        bool UpdateCheckpoint(Checkpoint checkpoint);
        bool DeleteCheckpoint(Checkpoint checkpoint);
        Checkpoint GetCheckpoint(int checkpointId);
        IList<int> FindCheckpoints(int cashgameId);
        IList<int> FindCheckpoints(IList<int> cashgameIds);
	}
}