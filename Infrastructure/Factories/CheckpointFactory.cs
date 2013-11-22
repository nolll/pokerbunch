using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class CheckpointFactory : ICheckpointFactory
    {
        public Checkpoint Create(RawCheckpoint rawCheckpoint)
        {
            return new Checkpoint
            (
                rawCheckpoint.Timestamp,
                (CheckpointType)rawCheckpoint.Type,
                rawCheckpoint.Stack,
                rawCheckpoint.Amount,
                rawCheckpoint.Id
            );
        }
    }
}