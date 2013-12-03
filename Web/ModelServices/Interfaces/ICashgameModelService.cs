using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;

namespace Web.ModelServices
{
    public interface ICashgameModelService
    {
        CashgameMatrixPageModel GetMatrixModel(string gameName, int? year = null);
        CashgameLeaderboardPageModel GetLeaderboardModel(string gameName, LeaderboardSortOrder sortOrder = LeaderboardSortOrder.winnings, int? year = null);
        string GetIndexUrl(string gameName);
        CashgameDetailsPageModel GetDetailsModel(string gameName, string dateStr);
        ChartModel GetDetailsChartJsonModel(string gameName, string dateStr);
        CashgameFactsPageModel GetFactsModel(string gameName, int? year = null);
        AddCashgamePageModel GetAddModel(string gameName);
        CashgameEditPageModel GetEditModel(string gameName, string dateStr);
    }
}