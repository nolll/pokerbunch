using System.Collections.Generic;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Interfaces
{
	public interface ICheckpointStorage
    {
		int AddCheckpoint(RawCheckpoint checkpoint);
        bool UpdateCheckpoint(RawCheckpoint checkpoint);
        bool DeleteCheckpoint(int id);
        IList<RawCheckpoint> GetCheckpoints(int cashgameId);
        IList<RawCheckpoint> GetCheckpoints(IList<int> cashgameIdList);
	    RawCheckpoint GetCheckpoint(int checkpointId);
	}
}