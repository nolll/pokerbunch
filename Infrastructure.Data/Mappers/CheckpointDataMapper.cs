using Application.Factories;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class CheckpointDataMapper : ICheckpointDataMapper
    {
        private readonly ICheckpointFactory _checkpointFactory;

        public CheckpointDataMapper(ICheckpointFactory checkpointFactory)
        {
            _checkpointFactory = checkpointFactory;
        }

        public Checkpoint Map(RawCheckpoint rawCheckpoint)
        {
            return _checkpointFactory.Create(
                rawCheckpoint.Timestamp,
                (CheckpointType)rawCheckpoint.Type,
                rawCheckpoint.Stack,
                rawCheckpoint.Amount,
                rawCheckpoint.Id);
        }
    }
}