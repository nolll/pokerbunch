using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgameTableModelFactory
    {
        RunningCashgameTableModel Create(Homegame homegame, Cashgame cashgame, bool isManager);
    }
}