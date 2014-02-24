using System;
using Core.Classes.Checkpoints;

namespace Application.Factories
{
    public class CheckpointFactory : ICheckpointFactory
    {
        public Checkpoint Create(
            DateTime timestamp,
            CheckpointType type,
            int stack,
            int amount,
            int id)
        {
            return new Checkpoint(timestamp, type, stack, amount, id);
        }
    }
}