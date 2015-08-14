using System;
using Core.UseCases;
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

        public RunningCashgameCheckpointJsonModel(RunningCashgame.RunningCashgameCheckpointItem item)
        {
            Time = item.Time;
            Stack = item.Stack;
            AddedMoney = item.AddedMoney;
        }
    }
}