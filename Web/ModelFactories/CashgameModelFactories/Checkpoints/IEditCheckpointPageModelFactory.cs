using Core.Entities;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageModelFactory
    {
        EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId);
        EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId, EditCheckpointPostModel postModel);
    }
}