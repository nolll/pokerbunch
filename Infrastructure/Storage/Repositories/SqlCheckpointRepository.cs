using Core.Entities.Checkpoints;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlCheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;

        public SqlCheckpointRepository(ICheckpointStorage checkpointStorage)
        {
            _checkpointStorage = checkpointStorage;
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            return _checkpointStorage.AddCheckpoint(rawCheckpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            return _checkpointStorage.UpdateCheckpoint(rawCheckpoint);
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointStorage.DeleteCheckpoint(checkpoint.Id);
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            var rawCheckpoint = _checkpointStorage.GetCheckpoint(checkpointId);
            return rawCheckpoint != null ? RawCheckpoint.CreateReal(rawCheckpoint) : null;
        }
    }
}