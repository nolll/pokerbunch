using Core.Classes;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageModelFactory
    {
        EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName);
        EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName, EditCheckpointPostModel postModel);
    }
}