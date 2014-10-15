using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Commands.CashgameCommands
{
    public interface ICashgameCommandProvider
    {
        Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId);
        Command GetCashoutCommand(string slug, int playerId, CashoutPostModel postModel);
        Command GetDeleteCommand(string slug, string dateStr);
        Command GetEditCheckpointCommand(string slug, string dateStr, int checkpointId, EditCheckpointPostModel postModel);
    }
}