using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

	public interface ICashgameSuiteFactory{

        CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players);

	}

}