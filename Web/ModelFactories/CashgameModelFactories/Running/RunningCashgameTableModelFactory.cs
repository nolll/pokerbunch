using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.Models.CashgameModels.Running;

namespace Web.ModelFactories.CashgameModelFactories.Running
{
    public class RunningCashgameTableModelFactory : IRunningCashgameTableModelFactory
    {
        private readonly IRunningCashgameTableItemModelFactory _runningCashgameTableItemModelFactory;
        private readonly IPlayerRepository _playerRepository;

        public RunningCashgameTableModelFactory(
            IRunningCashgameTableItemModelFactory runningCashgameTableItemModelFactory,
            IPlayerRepository playerRepository)
        {
            _runningCashgameTableItemModelFactory = runningCashgameTableItemModelFactory;
            _playerRepository = playerRepository;
        }

        public RunningCashgameTableModel Create(Homegame homegame, Cashgame cashgame, bool isManager)
        {
            var results = GetSortedResults(cashgame);
            var resultModels = new List<RunningCashgameTableItemModel>();
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                resultModels.Add(_runningCashgameTableItemModelFactory.Create(homegame, cashgame, player, result, isManager));
            }
            
            return new RunningCashgameTableModel
                {
                    StatusModels = resultModels,
                    TotalBuyin = Globalization.FormatCurrency(homegame.Currency, cashgame.Turnover),
                    TotalStacks = Globalization.FormatCurrency(homegame.Currency, cashgame.TotalStacks)
                };
        }

        private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
        }
    }
}