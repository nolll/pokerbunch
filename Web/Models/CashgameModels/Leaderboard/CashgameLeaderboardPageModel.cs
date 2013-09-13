using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardPageModel : PageProperties {

	    public CashgameLeaderboardTableModel TableModel { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }

		public CashgameLeaderboardPageModel (User user,
									Homegame homegame,
									CashgameSuite suite,
									List<int> years,
									int? year,
									Cashgame runningGame) : base(user, homegame, runningGame) {
			TableModel = new CashgameLeaderboardTableModel(homegame, suite);
			CashgameNavModel = new CashgameNavigationModel(homegame, "leaderboard", years, year, runningGame);
		}

        public override string BrowserTitle
        {
            get
            {
                return "Cashgame Leaderboard";
            }
        }

	}

}