using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Core.Services
{
    public class CheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;

        public CheckpointService(ICheckpointRepository checkpointRepository)
        {
            _checkpointRepository = checkpointRepository;
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointRepository.Add(checkpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointRepository.Update(checkpoint);
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointRepository.Delete(checkpoint);
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            return _checkpointRepository.Get(checkpointId);
        }
    }
}