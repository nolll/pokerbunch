using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public class CashgameLeaderboardPageModelFactory : ICashgameLeaderboardPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
        private readonly ICashgameLeaderboardTableModelFactory _cashgameLeaderboardTableModelFactory;

        public CashgameLeaderboardPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            ICashgameLeaderboardTableModelFactory cashgameLeaderboardTableModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
            _cashgameLeaderboardTableModelFactory = cashgameLeaderboardTableModelFactory;
        }

        public CashgameLeaderboardPageModel Create(User user, Homegame homegame, CashgameSuite suite, IList<int> years, int? year)
        {
            return new CashgameLeaderboardPageModel
                {
                    BrowserTitle = "Cashgame Leaderboard",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        TableModel = _cashgameLeaderboardTableModelFactory.Create(homegame, suite),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "leaderboard", years, year)
                };
        }
    }
}