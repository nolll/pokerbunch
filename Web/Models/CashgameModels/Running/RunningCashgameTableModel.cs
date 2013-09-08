using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;
using System.Linq;

namespace Web.Models.CashgameModels.Running{
    public class RunningCashgameTableModel{

	    public List<RunningCashgameTableItemModel> StatusModels { get; set; }
	    public string TotalBuyin { get; set; }
	    public string TotalStacks { get; set; }

		public RunningCashgameTableModel(Homegame homegame, Cashgame cashgame, bool isManager, ITimeProvider timeProvider = null){
			var results = GetSortedResults(cashgame);
			var resultModels = new List<RunningCashgameTableItemModel>();
			foreach(var result in results){
				resultModels.Add(new RunningCashgameTableItemModel(homegame, cashgame, result, isManager, timeProvider));
			}
			StatusModels = resultModels;
			TotalBuyin = Globalization.FormatCurrency(homegame.Currency, cashgame.Turnover);
			TotalStacks = Globalization.FormatCurrency(homegame.Currency, cashgame.TotalStacks);
		}

		private IEnumerable<CashgameResult> GetSortedResults(Cashgame cashgame){
			var results = cashgame.Results;
            return results.OrderByDescending(o => o.Winnings);
		}

	}

}