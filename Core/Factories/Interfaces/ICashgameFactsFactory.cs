using System.Collections.Generic;
using Core.Classes;

namespace Core.Factories{

	public interface ICashgameFactsFactory{

        CashgameFacts Create(IList<Cashgame> cashgames, IList<Player> players);

	}

}