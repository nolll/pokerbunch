using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

	public interface ICashgameFactory{

		Cashgame Create(string location, GameStatus? status, int? id = null, List<CashgameResult> results = null);

	}

}