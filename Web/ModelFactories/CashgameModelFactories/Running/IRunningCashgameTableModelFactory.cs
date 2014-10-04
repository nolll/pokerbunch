using System;
using System.Collections.Generic;
using Core.Entities;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public interface IRunningCashgameTableModelFactory
    {
        RunningCashgameTableModel Create(Bunch bunch, Cashgame cashgame, IList<Player> players, bool isManager, DateTime now);
    }
}