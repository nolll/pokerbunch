using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Facts{

	public class CashgameFactsPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }
	    public int GameCount { get; set; }
		public string TotalGameTime { get; set; }
		public string BestResultAmount { get; set; }
		public string BestResultName { get; set; }
		public string WorstResultAmount { get; set; }
		public string WorstResultName { get; set; }
		public string MostTimeDuration { get; set; }
		public string MostTimeName { get; set; }
	}

}