using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.List;
using Web.Models.CashgameModels.Report;
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
        AddCashgamePageModel GetAddModel(string slug, AddCashgamePostModel postModel = null);
        CashgameEditPageModel GetEditModel(string slug, string dateStr, CashgameEditPostModel postModel = null);
        RunningCashgamePageModel GetRunningModel(string slug);
        CashgameListPageModel GetListModel(string slug, int? year = null);
        CashgameChartPageModel GetChartModel(string slug, int? year = null);
        ChartModel GetChartJsonModel(string slug, int? year);
        ActionPageModel GetActionModel(string slug, string dateStr, string playerName);
        ChartModel GetActionChartJsonModel(string slug, string dateStr, string playerName);
        BuyinPageModel GetBuyinModel(string slug, string playerName, BuyinPostModel postModel = null);
        ReportPageModel GetReportModel(string slug, ReportPostModel postModel = null);
        CashoutPageModel GetCashoutModel(string slug, CashoutPostModel postModel = null);
        EndPageModel GetEndGameModel(string slug);
        EditCheckpointModel GetEditCheckpointModel(string slug, string dateStr, string playerName, int checkpointId);
    }
}