using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgamePageModelFactory
    {
        RunningCashgamePageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, IList<int> years, bool isManager, Cashgame runningGame = null);
    }
}