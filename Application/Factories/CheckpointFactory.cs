using System;
using Core.Entities.Checkpoints;

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
            switch (type)
            {
                case CheckpointType.Cashout:
                    return new CashoutCheckpoint(timestamp, stack, amount, id);
                case CheckpointType.Buyin:
                    return new BuyinCheckpoint(timestamp, stack, amount, id);
                default:
                    return new ReportCheckpoint(timestamp, stack, amount, id);
            }
        }
    }
}