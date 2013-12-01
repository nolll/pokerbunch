using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;
        private readonly ICacheBuster _cacheBuster;

        public CheckpointRepository(
            ICheckpointStorage checkpointStorage,
            IRawCheckpointFactory rawCheckpointFactory,
            ICacheBuster cacheBuster)
        {
            _checkpointStorage = checkpointStorage;
            _rawCheckpointFactory = rawCheckpointFactory;
            _cacheBuster = cacheBuster;
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
    }
}