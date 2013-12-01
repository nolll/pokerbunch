using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

	public interface ICashgameFactory
	{
	    Cashgame Create(RawCashgameWithResults rawGame);
	    Cashgame Create(RawCashgame rawGame, IEnumerable<RawCheckpoint> rawCheckpoints);
		Cashgame Create(string location, int? status, int? id = null, IList<CashgameResult> results = null);
	    IList<Cashgame> CreateList(IEnumerable<RawCashgameWithResults> rawGames);
	}

}