using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;

namespace Infrastructure.Data.Factories
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