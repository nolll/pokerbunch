using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

	public interface ICashgameFactory{

		Cashgame Create(string location, int? status, int? id = null, IList<CashgameResult> results = null);

	}

}