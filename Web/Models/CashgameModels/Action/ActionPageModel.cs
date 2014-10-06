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

        public ActionPageModel(BunchContextResult contextResult, ActionsResult actionsResult)
            : base("Player Actions", contextResult)
        {
            var date = Globalization.FormatShortDate(actionsResult.Date, true);
            Heading = string.Format("Cashgame {0}, {1}", date, actionsResult.PlayerName);
            Checkpoints = GetCheckpointModels(actionsResult);
            ChartDataUrl = actionsResult.ChartDataUrl;
        }

        private List<CheckpointModel> GetCheckpointModels(ActionsResult actionsResult)
        {
            return actionsResult.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}