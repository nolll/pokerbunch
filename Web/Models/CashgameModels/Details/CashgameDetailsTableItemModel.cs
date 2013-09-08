using System;
using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Details{
	
	public class CashgameDetailsTableItemModel{

	    public string Name { get; set; }
		public UrlModel PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Cashout { get; set; }
		public string Winnings { get; set; }
		public string WinningsClass { get; set; }
		public string Winrate { get; set; }

		public CashgameDetailsTableItemModel(Homegame homegame, Cashgame cashgame, CashgameResult result){
			if(result.Player != null){
				Name = result.Player.DisplayName;
				PlayerUrl = new CashgameActionUrlModel(homegame, cashgame, result.Player);
			}
			Buyin = Globalization.FormatCurrency(homegame.Currency, result.Buyin);
			Cashout = Globalization.FormatCurrency(homegame.Currency, result.Stack);
			Winnings = Globalization.FormatResult(homegame.Currency, result.Winnings);
			WinningsClass = Util.GetWinningsCssClass(result.Winnings);
			if(result.PlayedTime > 0){
				var winrate = (int)Math.Round((double)result.Winnings / result.PlayedTime * 60);
				Winrate = Globalization.FormatWinrate(homegame.Currency, winrate);
			} else {
				Winrate = string.Empty;
			}
		}

	}

}