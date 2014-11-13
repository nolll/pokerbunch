using Core.UseCases.BaseContext;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.RunningCashgame;
using Newtonsoft.Json;
using Web.Models.CashgameModels.Details;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Board
{
    public class CashgameBoardPageModel : PageModel
    {
        public string StartTime { get; private set; }
        public bool ShowStartTime { get; private set; }
        public string Location { get; private set; }
        public CashgameBoardTableModel TableModel { get; private set; }
        public bool ShowTable { get; private set; }
        public string ChartJson { get; private set; }
        public bool ShowChart { get; private set; }

        public CashgameBoardPageModel(BaseContextResult contextResult, RunningCashgameResult runningCashgameResult, CashgameDetailsChartResult cashgameDetailsChartResult)
            : base("Cashgame Board", contextResult)
	    {
	        Location = runningCashgameResult.Location;
	        ShowStartTime = runningCashgameResult.ShowStartTime;
            StartTime = runningCashgameResult.StartTime;
            TableModel = runningCashgameResult.IsStarted ? new CashgameBoardTableModel(runningCashgameResult) : null;
            ShowTable = runningCashgameResult.ShowTable;
            ShowChart = runningCashgameResult.ShowChart;
            ChartJson = JsonConvert.SerializeObject(new DetailsChartModel(cashgameDetailsChartResult));
	    }
    }
}