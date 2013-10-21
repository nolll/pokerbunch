using Core.Classes;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IBarModelFactory
    {
        BarModel Create(Homegame homegame, Cashgame runningGame);
    }
}