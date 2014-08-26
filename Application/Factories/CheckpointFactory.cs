using System;
using Core.Entities.Checkpoints;

namespace Application.Factories
{
    public static class CheckpointFactory
    {
        public static Checkpoint Create(
            DateTime timestamp,
            CheckpointType type,
            int stack,
            int amount = 0,
            int id = 0)
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