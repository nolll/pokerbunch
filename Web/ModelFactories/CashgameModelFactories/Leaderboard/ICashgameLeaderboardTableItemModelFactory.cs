using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public interface ICashgameLeaderboardTableItemModelFactory
    {
        CashgameLeaderboardTableItemModel Create(Homegame homegame, Player player, CashgameTotalResult result, int rank, LeaderboardSortOrder sortOrder);
    }
}