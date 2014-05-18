using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgamePageModelFactory
    {
        RunningCashgamePageModel Create(Homegame homegame, Cashgame cashgame, Player player, bool isManager);
    }
}