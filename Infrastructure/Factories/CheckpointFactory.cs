using System;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class CheckpointFactory : ICheckpointFactory
    {
        public Checkpoint Create(RawCheckpoint rawCheckpoint, TimeZoneInfo timeZone)
        {
            return new Checkpoint
            {
                Type = (CheckpointType)rawCheckpoint.Type,
                Timestamp = TimeZoneInfo.ConvertTime(rawCheckpoint.Timestamp, timeZone),
                Stack = rawCheckpoint.Stack,
                Amount = rawCheckpoint.Amount,
                Id = 1
            };
        }
    }
}