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
            : base("Player Actions", contextResult)
        {
            var date = Globalization.FormatShortDate(actionsResult.Date, true);
            Heading = string.Format("Cashgame {0}, {1}", date, actionsResult.PlayerName);
            Checkpoints = GetCheckpointModels(actionsResult);
            ChartJson = JsonHelper.Serialize(new ActionChartModel(actionsChartResult));
        }

        private List<CheckpointModel> GetCheckpointModels(Actions.Result actionsResult)
        {
            return actionsResult.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}