using Core.Entities;
using Core.Entities.Checkpoints;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public interface ICheckpointModelFactory
    {
        CheckpointModel Create(Homegame homegame, Cashgame cashgame, Player player, Checkpoint checkpoint, Role role);
    }
}