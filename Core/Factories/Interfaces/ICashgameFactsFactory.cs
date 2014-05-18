using System.Collections.Generic;
using Core.Entities;

namespace Core.Factories.Interfaces{

	public interface ICashgameFactsFactory{

        CashgameFacts Create(IList<Cashgame> cashgames, IList<Player> players);

	}

}