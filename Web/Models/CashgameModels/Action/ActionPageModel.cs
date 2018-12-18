using Core.Services;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.Action
{
    public class ActionPageModel : BunchPageModel
    {
        public CheckpointListModel Checkpoints { get; }
        public string ChartJson { get; }
        public string Heading { get; }

        public ActionPageModel(BunchContext.Result contextResult, Actions.Result actionsResult, ActionsChart.Result actionsChartResult)
            : base(contextResult)
        {
            var date = Globalization.FormatShortDate(actionsResult.Date, true);
            var playerName = actionsResult.PlayerName;
            Heading = $"Cashgame {date}, {playerName}";
            Checkpoints = new CheckpointListModel(actionsResult);
            ChartJson = JsonHelper.Serialize(new ActionChartModel(actionsChartResult));
        }

        public override string BrowserTitle => "Player Actions";

        public override View GetView()
        {
            return new View("~/Views/Pages/CashgameAction/Action.cshtml");
        }
    }
}