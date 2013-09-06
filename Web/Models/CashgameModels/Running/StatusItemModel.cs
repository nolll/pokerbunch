using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Running{

	public class StatusItemModel{

	    public string Name { get; set; }
	    public UrlModel PlayerUrl { get; set; }
		public string Buyin { get; set; }
		public string Stack { get; set; }
		public string Winnings { get; set; }
		public string Time { get; set; }
		public string WinningsClass { get; set; }
		public bool ManagerButtonsEnabled { get; set; }
		public UrlModel BuyinUrl { get; set; }
		public UrlModel ReportUrl { get; set; }
		public UrlModel CashoutUrl { get; set; }
		public bool HasCashedOut { get; set; }

		public StatusItemModel(Homegame homegame, Cashgame cashgame, CashgameResult result, bool isManager, ITimeProvider timeProvider){
			var player = result.Player;
			if(player != null){
				Name = player.DisplayName;
				PlayerUrl = new CashgameActionUrlModel(homegame, cashgame, player);
				if(isManager){
					BuyinUrl = new CashgameBuyinUrlModel(homegame, player);
					ReportUrl = new CashgameReportUrlModel(homegame, player);
					CashoutUrl = new CashgameCashoutUrlModel(homegame, player);
				}
			}
			Buyin = Globalization.FormatCurrency(homegame.Currency, result.Buyin);
			Stack = Globalization.FormatCurrency(homegame.Currency, result.Stack);
			Winnings = Globalization.FormatResult(homegame.Currency, result.Winnings);
			var lastReportedTime = result.LastReportTime;
			if(lastReportedTime.HasValue){
				var timespan = timeProvider.GetTime() - lastReportedTime.Value;
				Time = Globalization.FormatTimespan(timespan);
			}
			WinningsClass = Util.GetWinningsCssClass(result.Winnings);
			HasCashedOut = result.CashoutTime != null;
			ManagerButtonsEnabled = isManager;
		}

	}

}