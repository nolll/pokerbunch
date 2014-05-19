using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Factories.Interfaces;

namespace Core.Factories{

	public class CashgameTotalResultFactory : ICashgameTotalResultFactory
    {
        public IList<CashgameTotalResult> CreateList(IList<Player> players, IList<Cashgame> cashgames)
        {
            var list = players.Select(player => new CashgameTotalResult(player, cashgames)).ToList();
            return list.Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
        }
	}

}