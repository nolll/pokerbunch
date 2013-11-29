using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsTableModelFactory : ICashgameDetailsTableModelFactory
    {
        private readonly ICashgameDetailsTableItemModelFactory _cashgameDetailsTableItemModelFactory;
        private readonly IPlayerRepository _playerRepository;

        public CashgameDetailsTableModelFactory(
            ICashgameDetailsTableItemModelFactory cashgameDetailsTableItemModelFactory,
            IPlayerRepository playerRepository)
        {
            _cashgameDetailsTableItemModelFactory = cashgameDetailsTableItemModelFactory;
            _playerRepository = playerRepository;
        }

        public CashgameDetailsTableModel Create(Homegame homegame, Cashgame cashgame)
        {
            var results = GetSortedResults(cashgame);
            var resultModels = new List<CashgameDetailsTableItemModel>();
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.PlayerId);
                resultModels.Add(_cashgameDetailsTableItemModelFactory.Create(homegame, cashgame, player, result));
            }

            return new CashgameDetailsTableModel
                {
                    ResultModels = resultModels
                };
        }

        private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
        {
            return cashgame.Results.OrderByDescending(o => o.Winnings);
        }
    }
}