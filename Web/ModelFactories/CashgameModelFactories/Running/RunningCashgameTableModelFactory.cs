using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.System;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableModelFactory : IRunningCashgameTableModelFactory
    {
        private readonly IRunningCashgameTableItemModelFactory _runningCashgameTableItemModelFactory;

        public RunningCashgameTableModelFactory(
            IRunningCashgameTableItemModelFactory runningCashgameTableItemModelFactory)
        {
            _runningCashgameTableItemModelFactory = runningCashgameTableItemModelFactory;
        }

        public RunningCashgameTableModel Create(Homegame homegame, Cashgame cashgame, bool isManager)
        {
            var results = GetSortedResults(cashgame);
            var resultModels = new List<RunningCashgameTableItemModel>();
            foreach (var result in results)
            {
                resultModels.Add(_runningCashgameTableItemModelFactory.Create(homegame, cashgame, result, isManager));
            }
            
            return new RunningCashgameTableModel
                {
                    StatusModels = resultModels,
                    TotalBuyin = StaticGlobalization.FormatCurrency(homegame.Currency, cashgame.Turnover),
                    TotalStacks = StaticGlobalization.FormatCurrency(homegame.Currency, cashgame.TotalStacks)
                };
        }

        private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
        }
    }
}