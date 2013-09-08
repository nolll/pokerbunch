using System.Collections.Generic;
using Core.Classes;

namespace Web.Models.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableModel{

	    public List<CashgameLeaderboardTableItemModel> ItemModels { get; set; }

		public CashgameLeaderboardTableModel(Homegame homegame, CashgameSuite suite){
			ItemModels = GetItemModels(homegame, suite);
		}

		private List<CashgameLeaderboardTableItemModel> GetItemModels(Homegame homegame, CashgameSuite suite){
			var results = suite.TotalResults;
			var models = new List<CashgameLeaderboardTableItemModel>();
			var rank = 1;
			foreach(var result in results){
				models.Add(new CashgameLeaderboardTableItemModel(homegame, result, rank));
				rank++;
			}
			return models;
		}

	}

}