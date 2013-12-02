using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Facts{

	public class CashgameFactsPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public int GameCount { get; set; }
		public string TotalGameTime { get; set; }
        public string TotalTurnover { get; set; }
		public string BestResultAmount { get; set; }
		public string BestResultName { get; set; }
		public string WorstResultAmount { get; set; }
		public string WorstResultName { get; set; }
		public string MostTimeDuration { get; set; }
		public string MostTimeName { get; set; }
        public string BestTotalWinningsName { get; set; }
        public object BestTotalWinningsAmount { get; set; }
        public string WorstTotalWinningsName { get; set; }
        public object WorstTotalWinningsAmount { get; set; }
        public string BiggestTotalBuyinName { get; set; }
        public object BiggestTotalBuyinAmount { get; set; }
        public string BiggestTotalCashoutName { get; set; }
        public object BiggestTotalCashoutAmount { get; set; }
	}

}