using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.Toplist;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;

namespace Web.ModelServices
{
    public interface ICashgameModelService
    {
        CashgameMatrixPageModel GetMatrixModel(string slug, int? year = null);
        CashgameToplistPageModel GetToplistModel(string slug, ToplistSortOrder sortOrder = ToplistSortOrder.winnings, int? year = null);
        string GetIndexUrl(string slug);
        CashgameDetailsPageModel GetDetailsModel(string slug, string dateStr);
        ChartModel GetDetailsChartJsonModel(string slug, string dateStr);
        CashgameFactsPageModel GetFactsModel(string slug, int? year = null);
        AddCashgamePageModel GetAddModel(string slug);
        CashgameEditPageModel GetEditModel(string slug, string dateStr);
    }
}