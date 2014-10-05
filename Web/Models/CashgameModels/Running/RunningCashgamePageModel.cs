using Application.Urls;
using Application.UseCases.CashgameContext;
using Application.UseCases.RunningCashgame;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePageModel : CashgamePageModel
    {
        public string StartTime { get; private set; }
        public bool ShowStartTime { get; private set; }
        public string Location { get; private set; }
        public bool BuyinButtonEnabled { get; private set; }
        public bool ReportButtonEnabled { get; private set; }
        public bool CashoutButtonEnabled { get; private set; }
        public bool EndGameButtonEnabled { get; private set; }
        public Url BuyinUrl { get; private set; }
        public Url ReportUrl { get; private set; }
        public Url CashoutUrl { get; private set; }
        public Url EndGameUrl { get; private set; }
        public RunningCashgameTableModel RunningCashgameTableModel { get; set; }
        public bool ShowTable { get; private set; }
        public Url ChartDataUrl { get; private set; }
        public bool ShowChart { get; private set; }

	    public RunningCashgamePageModel(CashgameContextResult contextResult, RunningCashgameResult runningCashgameResult)
            : base("Running Cashgame", contextResult)
	    {
	        Location = runningCashgameResult.Location;
	        BuyinUrl = runningCashgameResult.BuyinUrl;
	        ReportUrl = runningCashgameResult.ReportUrl;
	        CashoutUrl = runningCashgameResult.CashoutUrl;
	        EndGameUrl = runningCashgameResult.EndGameUrl;
	        ShowStartTime = runningCashgameResult.ShowStartTime;
            StartTime = runningCashgameResult.StartTime;
            BuyinButtonEnabled = runningCashgameResult.BuyinButtonEnabled;
            ReportButtonEnabled = runningCashgameResult.ReportButtonEnabled;
            CashoutButtonEnabled = runningCashgameResult.CashoutButtonEnabled;
            EndGameButtonEnabled = runningCashgameResult.EndGameButtonEnabled;
            RunningCashgameTableModel = runningCashgameResult.IsStarted ? new RunningCashgameTableModel(runningCashgameResult) : null;
            ShowTable = runningCashgameResult.ShowTable;
            ShowChart = runningCashgameResult.ShowChart;
            ChartDataUrl = runningCashgameResult.ChartDataUrl;
	    }
    }
}