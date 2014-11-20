using System;
using Core.UseCases.RunningCashgame;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameCheckpointJsonModel
    {
        [UsedImplicitly]
        public DateTime Time { get; private set; }

        [UsedImplicitly]
        public int Stack { get; private set; }
        
        [UsedImplicitly]
        public int AddedMoney { get; private set; }

        public RunningCashgameCheckpointJsonModel(RunningCashgameCheckpointItem item)
        {
            Time = item.Time;
            Stack = item.Stack;
            AddedMoney = item.AddedMoney;
        }
    }
}