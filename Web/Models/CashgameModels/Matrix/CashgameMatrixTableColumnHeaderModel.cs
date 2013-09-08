using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixTableColumnHeaderModel{

	    public string Date { get; set; }
	    public UrlModel CashgameUrl { get; set; }

		public CashgameMatrixTableColumnHeaderModel(Homegame homegame, Cashgame cashgame, bool showYear = false){
            Date = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty;
			CashgameUrl = new CashgameDetailsUrlModel(homegame, cashgame);
		}

	}

}