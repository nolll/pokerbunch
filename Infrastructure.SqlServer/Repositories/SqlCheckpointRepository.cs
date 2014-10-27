using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Repositories
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

        public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            var id = _checkpointStorage.AddCheckpoint(rawCheckpoint);
            _cacheBuster.CashgameUpdated(cashgame.Id);
            return id;
        }

        public bool UpdateCheckpoint(Cashgame cashgame, Checkpoint checkpoint)
        {
            var rawCheckpoint = RawCheckpoint.Create(checkpoint);
            var success = _checkpointStorage.UpdateCheckpoint(rawCheckpoint);
            _cacheBuster.CashgameUpdated(cashgame.Id);
            return success;
        }

        public bool DeleteCheckpoint(Cashgame cashgame, int id)
        {
            var success = _checkpointStorage.DeleteCheckpoint(id);
            _cacheBuster.CashgameUpdated(cashgame.Id);
            return success;
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            var rawCheckpoint = _checkpointStorage.GetCheckpoint(checkpointId);
            return rawCheckpoint != null ? RawCheckpoint.CreateReal(rawCheckpoint) : null;
        }
    }
}