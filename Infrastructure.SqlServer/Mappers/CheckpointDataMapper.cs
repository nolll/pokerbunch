using Core.Entities.Checkpoints;
using Core.Factories;
using Infrastructure.SqlServer.Classes;

namespace Infrastructure.SqlServer.Mappers
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