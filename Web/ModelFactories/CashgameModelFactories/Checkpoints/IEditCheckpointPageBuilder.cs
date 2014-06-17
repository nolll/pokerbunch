using Core.Entities;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageBuilder
    {
        EditCheckpointPageModel Build(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId);
        EditCheckpointPageModel Build(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId, EditCheckpointPostModel postModel);
    }
}