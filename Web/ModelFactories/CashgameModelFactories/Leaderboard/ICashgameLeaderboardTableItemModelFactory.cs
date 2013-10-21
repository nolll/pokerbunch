using Core.Classes;
using Web.Models.CashgameModels.Leaderboard;

namespace Web.ModelFactories.CashgameModelFactories.Leaderboard
{
    public interface ICashgameLeaderboardTableItemModelFactory
    {
        CashgameLeaderboardTableItemModel Create(Homegame homegame, CashgameTotalResult result, int rank);
    }
}