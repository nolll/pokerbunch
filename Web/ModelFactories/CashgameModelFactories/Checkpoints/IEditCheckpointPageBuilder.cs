using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageBuilder
    {
        EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId);
        EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel);
    }
}