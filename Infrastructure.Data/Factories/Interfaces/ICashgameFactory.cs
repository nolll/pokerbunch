using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces{

	public interface ICashgameFactory
	{
	    Cashgame Create(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints);
		Cashgame Create(string location, int? status, int? id = null, IList<CashgameResult> results = null);
	    IList<Cashgame> CreateList(IEnumerable<RawCashgameWithResults> rawGames);
	}

}