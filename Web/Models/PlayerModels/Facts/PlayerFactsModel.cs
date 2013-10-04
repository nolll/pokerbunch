using System.Collections.Generic;
using Core.Classes;
using Infrastructure.System;

namespace Web.Models.PlayerModels.Facts{

	public class PlayerFactsModel{

	    public string Winnings { get; set; }
	    public string BestResult { get; set; }
	    public string WorstResult { get; set; }
	    public int GamesPlayed { get; set; }
	    public string TimePlayed { get; set; }

        public PlayerFactsModel(Homegame homegame, IEnumerable<Cashgame> cashgames, Player player)
        {
			var filteredGames = FilterCashgames(cashgames, player);
			Winnings = Globalization.FormatResult(homegame.Currency, GetWinnings(filteredGames, player));
			BestResult = Globalization.FormatResult(homegame.Currency, GetBestResult(filteredGames, player));
			WorstResult = Globalization.FormatResult(homegame.Currency, GetWorstResult(filteredGames, player));
			GamesPlayed = filteredGames.Count;
			TimePlayed = Globalization.FormatDuration(GetMinutesPlayed(filteredGames));
		}

		public List<Cashgame> FilterCashgames(IEnumerable<Cashgame> cashgames, Player player){
			var filteredCashgames = new List<Cashgame>();
			foreach(var cashgame in cashgames){
				if(cashgame.IsInGame(player)){
					filteredCashgames.Add(cashgame);
				}
			}
			return filteredCashgames;
		}

		public int GetWinnings(IEnumerable<Cashgame> cashgames, Player player){
			var winnings = 0;
			foreach(var cashgame in cashgames){
				var result = cashgame.GetResult(player);
				if(result != null){
					winnings += result.Winnings;
				}
			}
			return winnings;
		}

		public int GetBestResult(IEnumerable<Cashgame> cashgames, Player player){
			int? best = null;
			foreach(var cashgame in cashgames){
				var result = cashgame.GetResult(player);
				if(!best.HasValue || result != null && result.Winnings > best){
					best = result.Winnings;
				}
			}
			return best.HasValue ? best.Value : 0;
		}

		public int GetWorstResult(IEnumerable<Cashgame> cashgames, Player player){
			int? worst = null;
			foreach(var cashgame in cashgames){
				var result = cashgame.GetResult(player);
				if(!worst.HasValue || result != null && result.Winnings < worst){
					worst = result.Winnings;
				}
			}
			return worst.HasValue ? worst.Value : 0;
		}

		public int GetMinutesPlayed(IEnumerable<Cashgame> cashgames){
			var timePlayed = 0;
			foreach(var cashgame in cashgames){
				timePlayed += cashgame.Duration;
			}
			return timePlayed;
		}

	}

}