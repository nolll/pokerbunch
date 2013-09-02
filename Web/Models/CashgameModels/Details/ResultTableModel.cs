using System.Collections.Generic;
using Core.Classes;
using System.Linq;

namespace Web.Models.CashgameModels.Details{

	public class ResultTableModel{

	    public List<ResultTableItemModel> ResultModels { get; set; }

		public ResultTableModel(Homegame homegame, Cashgame cashgame){
			var results = GetSortedResults(cashgame);
			var resultModels = new List<ResultTableItemModel>();
			foreach(var result in results){
				resultModels.Add(new ResultTableItemModel(homegame, cashgame, result));
			}
			ResultModels = resultModels;
		}

		private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame)
		{
		    return cashgame.Results.OrderByDescending(o => o.Winnings);
		}

	}

}