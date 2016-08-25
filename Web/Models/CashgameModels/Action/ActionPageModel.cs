using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.UseCases;
using Web.Common.Services;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Action
{
    public class ActionPageModel : BunchPageModel
    {
        public List<CheckpointModel> Checkpoints { get; private set; }
        public string ChartJson { get; private set; }
        public string Heading { get; private set; }

        public ActionPageModel(BunchContext.Result contextResult, Actions.Result actionsResult, ActionsChart.Result actionsChartResult)
            : base(contextResult)
        {
            var date = Globalization.FormatShortDate(actionsResult.Date, true);
            var playerName = actionsResult.PlayerName;
            Heading = $"Cashgame {date}, {playerName}";
            Checkpoints = GetCheckpointModels(actionsResult);
            ChartJson = JsonHelper.Serialize(new ActionChartModel(actionsChartResult));
        }

        public override string BrowserTitle => "Player Actions";

        private List<CheckpointModel> GetCheckpointModels(Actions.Result actionsResult)
        {
            return actionsResult.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}