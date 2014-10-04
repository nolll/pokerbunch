using System;
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
        private readonly IPlayerRepository _playerRepository;

        public RunningCashgameTableModelFactory(
            IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public RunningCashgameTableModel Create(Bunch bunch, Cashgame cashgame, IList<Player> players, bool isManager, DateTime now)
        {
            var results = GetSortedResults(cashgame);
            var resultModels = new List<RunningCashgameTableItemModel>();
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                resultModels.Add(new RunningCashgameTableItemModel(bunch, cashgame, player, result, isManager, now));
            }
            
            return new RunningCashgameTableModel
                {
                    StatusModels = resultModels,
                    TotalBuyin = Globalization.FormatCurrency(bunch.Currency, cashgame.Turnover),
                    TotalStacks = Globalization.FormatCurrency(bunch.Currency, cashgame.TotalStacks)
                };
        }

        private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
        }
    }
}