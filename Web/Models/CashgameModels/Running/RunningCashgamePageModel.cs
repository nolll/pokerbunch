using Application.Urls;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Running
{
	public class RunningCashgamePageModel : IPageModel
    {
	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
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
	}
}