using Core.Classes;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IRunningCashgameTableItemModelFactory
    {
        RunningCashgameTableItemModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result, bool isManager);
    }
}