using Core.Entities.Checkpoints;
using Core.Factories;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public static class CheckpointDataMapper
    {
        public static Checkpoint Map(RawCheckpoint rawCheckpoint)
        {
            return CheckpointFactory.Create(
                rawCheckpoint.Timestamp,
                (CheckpointType)rawCheckpoint.Type,
                rawCheckpoint.Stack,
                rawCheckpoint.Amount,
                rawCheckpoint.Id);
        }
    }
}