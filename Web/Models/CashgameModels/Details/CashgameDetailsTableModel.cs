using System.Collections.Generic;
using Core.Classes;
using System.Linq;

namespace Web.Models.CashgameModels.Details{

	public class CashgameDetailsTableModel{

	    public List<CashgameDetailsTableItemModel> ResultModels { get; set; }

		public CashgameDetailsTableModel(Homegame homegame, Cashgame cashgame){
			var results = GetSortedResults(cashgame);
			var resultModels = new List<CashgameDetailsTableItemModel>();
			foreach(var result in results){
				resultModels.Add(new CashgameDetailsTableItemModel(homegame, cashgame, result));
			}
			ResultModels = resultModels;
		}

		private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
		{
		    return cashgame.Results.OrderByDescending(o => o.Winnings);
		}

	}

}