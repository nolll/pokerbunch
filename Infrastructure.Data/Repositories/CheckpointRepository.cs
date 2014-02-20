using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;
        private readonly ICacheBuster _cacheBuster;
        private readonly ICheckpointFactory _checkpointFactory;

        public CheckpointRepository(
            ICheckpointStorage checkpointStorage,
            IRawCheckpointFactory rawCheckpointFactory,
            ICacheBuster cacheBuster,
            ICheckpointFactory checkpointFactory)
        {
            _checkpointStorage = checkpointStorage;
            _rawCheckpointFactory = rawCheckpointFactory;
            _cacheBuster = cacheBuster;
            _checkpointFactory = checkpointFactory;
        }

        public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var rawCheckpoint = _rawCheckpointFactory.Create(cashgame, checkpoint);
            var id = _checkpointStorage.AddCheckpoint(cashgame.Id, player.Id, rawCheckpoint);
            _cacheBuster.CashgameUpdated(cashgame);
            return id;
        }

        public bool UpdateCheckpoint(Cashgame cashgame, Checkpoint checkpoint)
        {
            var rawCheckpoint = _rawCheckpointFactory.Create(cashgame, checkpoint);
            var success = _checkpointStorage.UpdateCheckpoint(rawCheckpoint);
            _cacheBuster.CashgameUpdated(cashgame);
            return success;
        }

        public bool DeleteCheckpoint(Cashgame cashgame, int id)
        {
            var success = _checkpointStorage.DeleteCheckpoint(id);
            _cacheBuster.CashgameUpdated(cashgame);
            return success;
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            var rawCheckpoint = _checkpointStorage.GetCheckpoint(checkpointId);
            return rawCheckpoint != null ? _checkpointFactory.Create(rawCheckpoint) : null;
        }
    }
}