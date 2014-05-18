using Core.Entities;
using Core.Entities.Checkpoints;

namespace Core.Repositories {

	public interface ICheckpointRepository{

		int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint);
        bool UpdateCheckpoint(Cashgame cashgame, Checkpoint checkpoint);
        bool DeleteCheckpoint(Cashgame cashgame, int id);
	    Checkpoint GetCheckpoint(int checkpointId);
	}
}