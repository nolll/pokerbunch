using Core.Classes;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageModelFactory
    {
        EditCheckpointPageModel Create(User user, Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName);
        EditCheckpointPageModel Create(User user, Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName, EditCheckpointPostModel postModel);
    }
}