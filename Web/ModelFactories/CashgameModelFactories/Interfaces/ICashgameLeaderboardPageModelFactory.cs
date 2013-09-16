using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICashgameLeaderboardPageModelFactory
    {
        CashgameLeaderboardPageModel Create(User user, Homegame homegame, CashgameSuite suite, List<int> years, int? year, Cashgame runningGame);
    }
}