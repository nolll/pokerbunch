using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.List;
using Web.Models.CashgameModels.Running;
using Web.Models.CashgameModels.Toplist;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;

namespace Web.ModelServices
{
    public interface ICashgameModelService
    {
        CashgameMatrixPageModel GetMatrixModel(string slug, int? year = null);
        CashgameToplistPageModel GetToplistModel(string slug, int? year = null);
        string GetIndexUrl(string slug);
        CashgameDetailsPageModel GetDetailsModel(string slug, string dateStr);
        ChartModel GetDetailsChartJsonModel(string slug, string dateStr);
        CashgameFactsPageModel GetFactsModel(string slug, int? year = null);
        AddCashgamePageModel GetAddModel(string slug);
        CashgameEditPageModel GetEditModel(string slug, string dateStr);
        RunningCashgamePageModel GetRunningModel(string slug);
        CashgameListPageModel GetListModel(string slug, int? year = null);
        CashgameChartPageModel GetChartModel(string slug, int? year = null);
        ChartModel GetChartJsonModel(string slug, int? year);
        ActionPageModel GetActionModel(string slug, string dateStr, string playerName);
    }
}