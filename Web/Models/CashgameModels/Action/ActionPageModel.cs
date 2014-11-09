using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.UseCases.Actions;
using Core.UseCases.ActionsChart;
using Core.UseCases.BunchContext;
using Newtonsoft.Json;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Action
{
    public class ActionPageModel : BunchPageModel
    {
        public List<CheckpointModel> Checkpoints { get; private set; }
        public string ChartJson { get; private set; }
        public string Heading { get; private set; }

        public ActionPageModel(BunchContextResult contextResult, ActionsOutput actionsOutput, ActionsChartResult actionsChartResult)
            : base("Player Actions", contextResult)
        {
            var date = Globalization.FormatShortDate(actionsOutput.Date, true);
            Heading = string.Format("Cashgame {0}, {1}", date, actionsOutput.PlayerName);
            Checkpoints = GetCheckpointModels(actionsOutput);
            ChartJson = JsonConvert.SerializeObject(new ActionChartModel(actionsChartResult));
        }

        private List<CheckpointModel> GetCheckpointModels(ActionsOutput actionsOutput)
        {
            return actionsOutput.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}