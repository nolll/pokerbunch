using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameLeaderboardPageModelFactory : ICashgameLeaderboardPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;

        public CashgameLeaderboardPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
        }

        public CashgameLeaderboardPageModel Create(User user, Homegame homegame, CashgameSuite suite, IList<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameLeaderboardPageModel
                {
                    BrowserTitle = "Cashgame Leaderboard",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        TableModel = new CashgameLeaderboardTableModel(homegame, suite),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "leaderboard", years, year, runningGame)
                };
        }
    }
}