using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

	public interface ICashgameSuiteFactory{

		CashgameSuite Create(List<Cashgame> cashgames, List<Player> players);

	}

}