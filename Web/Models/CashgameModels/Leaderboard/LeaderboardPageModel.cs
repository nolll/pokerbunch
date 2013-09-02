using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class LeaderboardPageModel : HomegamePageModel {

	    public LeaderboardTableModel TableModel { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }

		public LeaderboardPageModel (User user,
									Homegame homegame,
									CashgameSuite suite,
									List<int> years,
									int? year,
									Cashgame runningGame) : base(user, homegame, runningGame) {
			TableModel = new LeaderboardTableModel(homegame, suite);
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