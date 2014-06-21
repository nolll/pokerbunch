using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
	public class RunningCashgamePageModel : PageModel
    {
        public string StartTime { get; set; }
        public bool ShowStartTime { get; set; }
        public string Location { get; set; }
        public bool BuyinButtonEnabled { get; set; }
        public bool ReportButtonEnabled { get; set; }
        public bool CashoutButtonEnabled { get; set; }
        public bool EndGameButtonEnabled { get; set; }
        public Url BuyinUrl { get; set; }
        public Url ReportUrl { get; set; }
        public Url CashoutUrl { get; set; }
        public Url EndGameUrl { get; set; }
        public RunningCashgameTableModel RunningCashgameTableModel { get; set; }
        public bool ShowTable { get; set; }
        public Url ChartDataUrl { get; set; }
        public bool ShowChart { get; set; }

	    public RunningCashgamePageModel(BunchContextResult contextResult)
            : base("Running Cashgame", contextResult)
	    {
	    }
    }
}