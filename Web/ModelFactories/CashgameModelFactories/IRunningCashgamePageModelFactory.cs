using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories
{
    public interface IRunningCashgamePageModelFactory
    {
        RunningCashgamePageModel Create(User user, Homegame homegame, Cashgame cashgame, Player player, List<int> years, bool isManager, ITimeProvider timer, Cashgame runningGame = null);
    }
}