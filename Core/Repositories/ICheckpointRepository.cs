using Core.Entities.Checkpoints;

namespace Core.Repositories
{
	public interface ICheckpointRepository
    {
		int Add(Checkpoint checkpoint);
        bool Update(Checkpoint checkpoint);
        bool Delete(Checkpoint checkpoint);
	    Checkpoint Get(int checkpointId);
	}
}