using System.Web;
using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableItemModel{

	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public string GameTime { get; set; }
	    public string WinRate { get; set; }
	    public UrlModel PlayerUrl { get; set; }

		public CashgameLeaderboardTableItemModel(Homegame homegame, CashgameTotalResult result, int rank){
			Rank = rank;
			if(result != null){
				var winnings = result.Winnings;
				TotalResult = Globalization.FormatResult(homegame.Currency, winnings);
				ResultClass = Util.GetWinningsCssClass(winnings);
				GameTime = Globalization.FormatDuration(result.TimePlayed);
				WinRate = Globalization.FormatWinrate(homegame.Currency, result.WinRate);
				var player = result.Player;
				if(player != null){
					Name = player.DisplayName;
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName);
					PlayerUrl = new PlayerDetailsUrlModel(homegame, player);
				}
			}
		}

	}

}