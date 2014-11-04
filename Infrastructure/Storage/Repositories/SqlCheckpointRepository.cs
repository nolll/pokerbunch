using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlCheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;
        private readonly ICacheBuster _cacheBuster;

        public SqlCheckpointRepository(
            ICheckpointStorage checkpointStorage,
            ICacheBuster cacheBuster)
        {
            _checkpointStorage = checkpointStorage;
            _cacheBuster = cacheBuster;
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            var id = _checkpointStorage.AddCheckpoint(rawCheckpoint);
            _cacheBuster.CashgameUpdated(checkpoint.CashgameId);
            return id;
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            var success = _checkpointStorage.UpdateCheckpoint(rawCheckpoint);
            _cacheBuster.CashgameUpdated(checkpoint.CashgameId);
            return success;
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            var success = _checkpointStorage.DeleteCheckpoint(checkpoint.Id);
            _cacheBuster.CashgameUpdated(checkpoint.CashgameId);
            return success;
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            var rawCheckpoint = _checkpointStorage.GetCheckpoint(checkpointId);
            return rawCheckpoint != null ? RawCheckpoint.CreateReal(rawCheckpoint) : null;
        }
    }
}