using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;

        public CheckpointRepository(ICheckpointStorage checkpointStorage)
        {
            _checkpointStorage = checkpointStorage;
        }

        public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            return _checkpointStorage.AddCheckpoint(cashgame, player, checkpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointStorage.UpdateCheckpoint(checkpoint);
        }

        public bool DeleteCheckpoint(int id)
        {
            return _checkpointStorage.DeleteCheckpoint(id);
        }
    }
}