using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.RunningCashgame;
using Newtonsoft.Json;
using Web.Models.CashgameModels.Details;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePageModel : BunchPageModel
    {
        public string StartTime { get; private set; }
        public bool ShowStartTime { get; private set; }
        public string Location { get; private set; }
        public bool BuyinButtonEnabled { get; private set; }
        public bool ReportButtonEnabled { get; private set; }
        public bool CashoutButtonEnabled { get; private set; }
        public bool EndGameButtonEnabled { get; private set; }
        public string BuyinUrl { get; private set; }
        public string ReportUrl { get; private set; }
        public string CashoutUrl { get; private set; }
        public string EndGameUrl { get; private set; }
        public RunningCashgameTableModel RunningCashgameTableModel { get; private set; }
        public bool ShowTable { get; private set; }
        public string ChartJson { get; private set; }
        public bool ShowChart { get; private set; }

	    public RunningCashgamePageModel(BunchContextResult contextResult, RunningCashgameResult runningCashgameResult, CashgameDetailsChartResult cashgameDetailsChartResult)
            : base("Running Cashgame", contextResult)
	    {
	        Location = runningCashgameResult.Location;
	        BuyinUrl = runningCashgameResult.BuyinUrl.Relative;
            ReportUrl = runningCashgameResult.ReportUrl.Relative;
            CashoutUrl = runningCashgameResult.CashoutUrl.Relative;
            EndGameUrl = runningCashgameResult.EndGameUrl.Relative;
	        ShowStartTime = runningCashgameResult.ShowStartTime;
            StartTime = runningCashgameResult.StartTime;
            BuyinButtonEnabled = runningCashgameResult.BuyinButtonEnabled;
            ReportButtonEnabled = runningCashgameResult.ReportButtonEnabled;
            CashoutButtonEnabled = runningCashgameResult.CashoutButtonEnabled;
            EndGameButtonEnabled = runningCashgameResult.EndGameButtonEnabled;
            RunningCashgameTableModel = runningCashgameResult.IsStarted ? new RunningCashgameTableModel(runningCashgameResult) : null;
            ShowTable = runningCashgameResult.ShowTable;
            ShowChart = runningCashgameResult.ShowChart;
            ChartJson = JsonConvert.SerializeObject(new DetailsChartModel(cashgameDetailsChartResult));
	    }
    }
}