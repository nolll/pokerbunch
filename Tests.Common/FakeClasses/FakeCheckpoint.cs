using System;
using Core.Classes.Checkpoints;

namespace Tests.Common.FakeClasses
{
    public class FakeCheckpoint : Checkpoint
    {
        public FakeCheckpoint(
            DateTime timestamp = default(DateTime),
            CheckpointType type = default(CheckpointType),
            int stack = default(int),
            int amount = default(int),
            int id = default(int))
            : base(
                timestamp,
                type,
                stack,
                amount,
                id)
        {
        }
    }
}