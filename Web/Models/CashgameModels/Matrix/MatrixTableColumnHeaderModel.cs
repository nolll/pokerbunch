using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;
using app;

namespace Web.Models.CashgameModels.Matrix{
    public class MatrixTableColumnHeaderModel{

	    public string Date { get; set; }
	    public UrlModel CashgameUrl { get; set; }

		public MatrixTableColumnHeaderModel(Homegame homegame, Cashgame cashgame, bool showYear = false){
            Date = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty;
			CashgameUrl = new CashgameDetailsUrlModel(homegame, cashgame);
		}

	}

}