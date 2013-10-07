using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Repositories;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointStorage _checkpointStorage;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;

        public CheckpointRepository(
            ICheckpointStorage checkpointStorage,
            IRawCheckpointFactory rawCheckpointFactory)
        {
            _checkpointStorage = checkpointStorage;
            _rawCheckpointFactory = rawCheckpointFactory;
        }

        public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint)
        {
            var rawCheckpoint = _rawCheckpointFactory.Create(checkpoint);
            return _checkpointStorage.AddCheckpoint(cashgame.Id, player.Id, rawCheckpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            var rawCheckpoint = _rawCheckpointFactory.Create(checkpoint);
            return _checkpointStorage.UpdateCheckpoint(rawCheckpoint);
        }

        public bool DeleteCheckpoint(int id)
        {
            return _checkpointStorage.DeleteCheckpoint(id);
        }
    }
}