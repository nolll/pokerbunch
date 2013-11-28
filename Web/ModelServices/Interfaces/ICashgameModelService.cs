using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelServices
{
    public interface ICashgameModelService
    {
        CashgameMatrixPageModel GetMatrixModel(string gameName, int? year = null);
        CashgameLeaderboardPageModel GetLeaderboardModel(string gameName, int? year = null);
    }
}