using System;
using Core.Entities.Checkpoints;

namespace Core.UseCases.RunningCashgame
{
    public class RunningCashgameCheckpointItem
    {
        public DateTime Time { get; private set; }
        public int Stack { get; private set; }
        public int AddedMoney { get; private set; }

        public RunningCashgameCheckpointItem(Checkpoint checkpoint)
        {
            Time = checkpoint.Timestamp;
            Stack = checkpoint.Stack;
            AddedMoney = checkpoint.Amount;
        }
    }
}