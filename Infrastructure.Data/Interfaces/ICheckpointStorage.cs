using System.Collections.Generic;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data
{
	public interface ICheckpointStorage
    {
		int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint);
        bool UpdateCheckpoint(RawCheckpoint checkpoint);
        bool DeleteCheckpoint(int id);
        IList<RawCheckpoint> GetCheckpoints(int cashgameId);
        IList<RawCheckpoint> GetCheckpoints(IList<int> cashgameIdList);
	    RawCheckpoint GetCheckpoint(int checkpointId);
	}
}