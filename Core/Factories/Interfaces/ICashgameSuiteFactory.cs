using System.Collections.Generic;
using Core.Classes;

namespace Core.Factories{

	public interface ICashgameSuiteFactory{

        CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players);

	}

}