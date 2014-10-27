using System;
using Core.Entities.Checkpoints;

namespace Core.Factories
{
    public static class CheckpointFactory
    {
        public static Checkpoint Create(
            int cashgameId,
            int playerId,
            DateTime timestamp,
            CheckpointType type,
            int stack,
            int amount = 0,
            int id = 0)
        {
            switch (type)
            {
                case CheckpointType.Cashout:
                    return new CashoutCheckpoint(cashgameId, playerId, timestamp, stack, amount, id);
                case CheckpointType.Buyin:
                    return new BuyinCheckpoint(cashgameId, playerId, timestamp, stack, amount, id);
                default:
                    return new ReportCheckpoint(cashgameId, playerId, timestamp, stack, amount, id);
            }
        }
    }
}