using System.Collections.Generic;
using System.Web;
using Core.Classes;
using Infrastructure.System;
using Web.Models.Url;

namespace Web.Models{
    public class RowModel{

	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public string GameTime { get; set; }
	    public string WinRate { get; set; }
	    public UrlModel PlayerUrl { get; set; }
        public List<CellModel> CellModels { get; set; }

		//public $player;
		//public $currency;
		
		public RowModel(Homegame homegame, CashgameSuite suite, CashgameTotalResult result, int rank){
			var cashgames = suite.Cashgames;
		    Rank = rank;

			if(result != null){
				var player = result.Player;
				if(player != null){
					Name = player.DisplayName;
					UrlEncodedName = HttpUtility.UrlEncode(player.DisplayName);
					PlayerUrl = new PlayerDetailsUrlModel(homegame, player);
					CellModels = GetCellModels(cashgames, player);
				}

				var winnings = result.Winnings;
				TotalResult = Globalization.FormatResult(homegame.Currency, winnings);
				ResultClass = Util.GetWinningsCssClass(winnings);
			}
		}

		private List<CellModel> GetCellModels(IEnumerable<Cashgame> cashgames, Player player)
		{
		    var models = new List<CellModel>();
			if(cashgames != null){
				foreach(var cashgame in cashgames){
					var result = cashgame.GetResult(player);
					models.Add(new CellModel(cashgame, result));
				}
			}
			return models;
		}

	}

}