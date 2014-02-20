using Core.Classes;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public interface IEditCheckpointPageModelFactory
    {
        EditCheckpointModel Create(User user, Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName);
    }
}