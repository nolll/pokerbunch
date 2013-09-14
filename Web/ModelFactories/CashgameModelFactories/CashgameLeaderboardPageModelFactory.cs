using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameLeaderboardPageModelFactory : ICashgameLeaderboardPageModelFactory
    {
        public CashgameLeaderboardPageModel Create(User user, Homegame homegame, CashgameSuite suite, List<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameLeaderboardPageModel
                {
                    BrowserTitle = "Cashgame Leaderboard",
                    PageProperties = new PageProperties(user, homegame, runningGame),
			        TableModel = new CashgameLeaderboardTableModel(homegame, suite),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "leaderboard", years, year, runningGame)
                };
        }
    }
}