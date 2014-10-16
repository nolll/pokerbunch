using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.Urls;
using Core.UseCases.Actions;
using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Action
{
    public class ActionPageModel : BunchPageModel
    {
        public List<CheckpointModel> Checkpoints { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public string Heading { get; private set; }

        public ActionPageModel(BunchContextResult contextResult, ActionsOutput actionsOutput)
            : base("Player Actions", contextResult)
        {
            var date = Globalization.FormatShortDate(actionsOutput.Date, true);
            Heading = string.Format("Cashgame {0}, {1}", date, actionsOutput.PlayerName);
            Checkpoints = GetCheckpointModels(actionsOutput);
            ChartDataUrl = actionsOutput.ChartDataUrl;
        }

        private List<CheckpointModel> GetCheckpointModels(ActionsOutput actionsOutput)
        {
            return actionsOutput.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}