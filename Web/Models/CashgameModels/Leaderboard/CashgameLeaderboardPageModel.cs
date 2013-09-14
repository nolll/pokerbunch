using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardPageModel : IPageModel {

        public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public CashgameLeaderboardTableModel TableModel { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }
	}

}