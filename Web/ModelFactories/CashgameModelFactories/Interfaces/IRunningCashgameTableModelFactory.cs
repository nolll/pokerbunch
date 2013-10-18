using Core.Classes;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IRunningCashgameTableModelFactory
    {
        RunningCashgameTableModel Create(Homegame homegame, Cashgame cashgame, bool isManager);
    }
}