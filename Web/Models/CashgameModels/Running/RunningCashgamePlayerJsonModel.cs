using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePlayerJsonModel
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public string Name { get; private set; }

        [UsedImplicitly]
        public string Url { get; private set; }

        [UsedImplicitly]
        public bool HasCashedOut { get; private set; }

        [UsedImplicitly]
        public IList<RunningCashgameCheckpointJsonModel> Checkpoints { get; private set; }

        public RunningCashgamePlayerJsonModel(RunningCashgame.RunningCashgamePlayerItem item)
        {
            Id = item.PlayerId;
            Name = item.Name;
            Url = item.PlayerUrl.Relative;
            HasCashedOut = item.HasCashedOut;
            Checkpoints = item.Checkpoints.Select(o => new RunningCashgameCheckpointJsonModel(o)).ToList();
        }
    }
}