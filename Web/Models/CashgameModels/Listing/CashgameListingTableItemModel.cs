using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingTableItemModel{

	    public int PlayerCount { get; set; }
		public string Location { get; set; }
		public string Duration { get; set; }
		public string Turnover { get; set; }
		public string AvgBuyin { get; set; }
		public UrlModel DetailsUrl { get; set; }
		public string DisplayDate { get; set; }
		public string PublishedClass { get; set; }

		public CashgameListingTableItemModel(Homegame homegame, Cashgame cashgame, bool showYear){
			var playerCount = cashgame.PlayerCount;
			PlayerCount = playerCount;
			Location = cashgame.Location;
			Duration = GetDuration(cashgame);
			Turnover = GetTurnover(homegame, cashgame);
			AvgBuyin = GetAvgBuyin(homegame, cashgame, playerCount);
			DetailsUrl = new CashgameDetailsUrlModel(homegame, cashgame);
			DisplayDate = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value, showYear) : null;
			PublishedClass = GetPublishedClass(cashgame);
		}

		private string GetDuration(Cashgame cashgame){
			var duration = cashgame.Duration;
			if(duration > 0){
				return Globalization.FormatDuration(duration);
			}
			return string.Empty;
		}

		private string GetTurnover(Homegame homegame, Cashgame cashgame){
			return Globalization.FormatCurrency(homegame.Currency, cashgame.Turnover);
		}

		private string GetAvgBuyin(Homegame homegame, Cashgame cashgame, int playerCount){
			return Globalization.FormatCurrency(homegame.Currency, cashgame.AverageBuyin);
		}

		private string GetPublishedClass(Cashgame cashgame){
			return cashgame.Status == GameStatus.Published ? string.Empty : "unpublished";
		}

	}

}