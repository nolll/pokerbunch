using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public interface ICashgameLeaderboardTableModelFactory
    {
        CashgameLeaderboardTableModel Create(Homegame homegame, CashgameSuite suite, int? year, LeaderboardSortOrder sortOrder);
    }
}