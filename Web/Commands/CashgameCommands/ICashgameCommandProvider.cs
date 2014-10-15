using Web.Models.CashgameModels.Checkpoints;

namespace Web.Commands.CashgameCommands
{
    public interface ICashgameCommandProvider
    {
        Command GetDeleteCheckpointCommand(string slug, string dateStr, int checkpointId);
        Command GetDeleteCommand(string slug, string dateStr);
        Command GetEditCheckpointCommand(string slug, string dateStr, int checkpointId, EditCheckpointPostModel postModel);
    }
}