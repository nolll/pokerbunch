using System.Collections.Generic;
using Core.Classes;

namespace Application.Factories{

	public interface ICashgameFactsFactory{

        CashgameFacts Create(IList<Cashgame> cashgames, IList<Player> players);

	}

}