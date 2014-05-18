using System.Collections.Generic;
using Core.Entities;

namespace Core.Factories.Interfaces
{
	public interface ICashgameSuiteFactory
    {
        CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players);
	}
}