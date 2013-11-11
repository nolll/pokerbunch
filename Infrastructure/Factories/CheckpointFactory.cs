using System;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class CheckpointFactory : ICheckpointFactory
    {
        public Checkpoint Create(RawCheckpoint rawCheckpoint)
        {
            return new Checkpoint
            {
                Type = (CheckpointType)rawCheckpoint.Type,
                Timestamp = rawCheckpoint.Timestamp,
                Stack = rawCheckpoint.Stack,
                Amount = rawCheckpoint.Amount,
                Id = rawCheckpoint.Id
            };
        }
    }
}