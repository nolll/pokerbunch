using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgameTableItemModelFactory
    {
        RunningCashgameTableItemModel Create(Bunch bunch, Cashgame cashgame, Player player, CashgameResult result, bool isManager);
    }
}