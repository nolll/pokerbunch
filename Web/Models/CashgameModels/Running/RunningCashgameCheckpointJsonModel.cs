using System;
using Core.UseCases;
using JetBrains.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgameCheckpointJsonModel
    {
        [UsedImplicitly]
        public string Type { get; private set; }

        [UsedImplicitly]
        public DateTime Time { get; private set; }

        [UsedImplicitly]
        public int Stack { get; private set; }
        
        [UsedImplicitly]
        public int AddedMoney { get; private set; }

        public RunningCashgameCheckpointJsonModel(RunningCashgame.RunningCashgameCheckpointItem item)
        {
            Type = item.Type.ToString().ToLower();
            Time = item.Time;
            Stack = item.Stack;
            AddedMoney = item.AddedMoney;
        }
    }
}