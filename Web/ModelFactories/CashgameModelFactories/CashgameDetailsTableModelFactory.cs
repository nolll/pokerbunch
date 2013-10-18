using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameDetailsTableModelFactory : ICashgameDetailsTableModelFactory
    {
        private readonly ICashgameDetailsTableItemModelFactory _cashgameDetailsTableItemModelFactory;

        public CashgameDetailsTableModelFactory(ICashgameDetailsTableItemModelFactory cashgameDetailsTableItemModelFactory)
        {
            _cashgameDetailsTableItemModelFactory = cashgameDetailsTableItemModelFactory;
        }

        public CashgameDetailsTableModel Create(Homegame homegame, Cashgame cashgame)
        {
            var results = GetSortedResults(cashgame);
            var resultModels = new List<CashgameDetailsTableItemModel>();
            foreach (var result in results)
            {
                resultModels.Add(_cashgameDetailsTableItemModelFactory.Create(homegame, cashgame, result));
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