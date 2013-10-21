using System.Collections.Generic;
using System.Web;
using Core.Classes;
using Infrastructure.System;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixTableRowModel{

	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public string GameTime { get; set; }
	    public string WinRate { get; set; }
	    public UrlModel PlayerUrl { get; set; }
        public List<CashgameMatrixTableCellModel> CellModels { get; set; }
		
		public CashgameMatrixTableRowModel(Homegame homegame, CashgameSuite suite, CashgameTotalResult result, int rank){
			var cashgames = suite.Cashgames;
		    Rank = rank;

			if(result != null){
				var player = result.Player;
				if(player != null){
					Name = player.DisplayName;
                    UrlEncodedName = HttpUtility.UrlPathEncode(player.DisplayName);
					PlayerUrl = new PlayerDetailsUrlModel(homegame, player);
					CellModels = GetCellModels(cashgames, player);
				}

				var winnings = result.Winnings;
				TotalResult = Globalization.FormatResult(homegame.Currency, winnings);
				ResultClass = Util.GetWinningsCssClass(winnings);
			}
		}

		private List<CashgameMatrixTableCellModel> GetCellModels(IEnumerable<Cashgame> cashgames, Player player)
		{
		    var models = new List<CashgameMatrixTableCellModel>();
			if(cashgames != null){
				foreach(var cashgame in cashgames){
					var result = cashgame.GetResult(player);
					models.Add(new CashgameMatrixTableCellModel(cashgame, result));
				}
			}
			return models;
		}

	}

}