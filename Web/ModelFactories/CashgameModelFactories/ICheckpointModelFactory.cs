using Core.Classes;
using Core.Classes.Checkpoints;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface ICheckpointModelFactory
    {
        CheckpointModel Create(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint, Role role);
    }
}