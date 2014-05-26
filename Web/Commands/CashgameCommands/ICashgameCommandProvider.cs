using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Report;

namespace Web.Commands.CashgameCommands
{
    public interface ICashgameCommandProvider
    {
        Command GetEndGameCommand(string slug);
        Command GetAddCommand(string slug, AddCashgamePostModel postModel);
        Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel);
        Command GetBuyinCommand(string slug, int playerId, BuyinPostModel postModel);
        Command GetReportCommand(string slug, int playerId, ReportPostModel postModel);
        Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId);
        Command GetCashoutCommand(string slug, int playerId, CashoutPostModel postModel);
        Command GetDeleteCommand(string slug, string dateStr);
        Command GetEditCheckpointCommand(string slug, string dateStr, int checkpointId, EditCheckpointPostModel postModel);
    }
}