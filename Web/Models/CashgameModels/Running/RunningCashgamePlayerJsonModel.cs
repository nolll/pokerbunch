using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using JetBrains.Annotations;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePlayerJsonModel
    {
        [UsedImplicitly]
        public string Id { get; private set; }

        [UsedImplicitly]
        public string Name { get; private set; }

        [UsedImplicitly]
        public string Url { get; private set; }

        [UsedImplicitly]
        public string Color { get; private set; }

        [UsedImplicitly]
        public bool HasCashedOut { get; private set; }

        [UsedImplicitly]
        public IList<RunningCashgameCheckpointJsonModel> Checkpoints { get; private set; }

        public RunningCashgamePlayerJsonModel(RunningCashgame.RunningCashgamePlayerItem item)
        {
            Id = item.PlayerId;
            Name = item.Name;
            Url = new CashgameActionUrl(item.CashgameId, item.PlayerId).Relative;
            Color = item.Color;
            HasCashedOut = item.HasCashedOut;
            Checkpoints = item.Checkpoints.Select(o => new RunningCashgameCheckpointJsonModel(o)).ToList();
        }
    }
}