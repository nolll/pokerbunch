using Core.Classes;
using Infrastructure.System;
using Web.Models.Url;
using app;

namespace Web.Models{
    public class ColumnHeaderModel{

	    public string Date { get; set; }
	    public UrlModel CashgameUrl { get; set; }

		public ColumnHeaderModel(Homegame homegame, Cashgame cashgame, bool showYear = false){
            Date = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : string.Empty;
			CashgameUrl = new CashgameDetailsUrlModel(homegame, cashgame);
		}

	}

}