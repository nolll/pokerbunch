using Core.Entities.Checkpoints;

namespace Core.Repositories
{
	public interface ICheckpointRepository
    {
		int AddCheckpoint(Checkpoint checkpoint);
        bool UpdateCheckpoint(Checkpoint checkpoint);
        bool DeleteCheckpoint(Checkpoint checkpoint);
	    Checkpoint GetCheckpoint(int checkpointId);
	}
}