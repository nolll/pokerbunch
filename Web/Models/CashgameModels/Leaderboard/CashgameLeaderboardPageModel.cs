using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardPageModel : IPageModel {

        public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public CashgameLeaderboardTableModel TableModel { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }

		public CashgameLeaderboardPageModel (User user,
									Homegame homegame,
									CashgameSuite suite,
									List<int> years,
									int? year,
									Cashgame runningGame)
		{
		    BrowserTitle = "Cashgame Leaderboard";
            PageProperties = new PageProperties(user, homegame, runningGame);
			TableModel = new CashgameLeaderboardTableModel(homegame, suite);
			CashgameNavModel = new CashgameNavigationModel(homegame, "leaderboard", years, year, runningGame);
		}

	}

}