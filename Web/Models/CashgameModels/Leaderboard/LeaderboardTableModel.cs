using System.Collections.Generic;
using Core.Classes;

namespace Web.Models.CashgameModels.Leaderboard{

	public class LeaderboardTableModel{

	    public List<LeaderboardTableItemModel> ItemModels { get; set; }

		public LeaderboardTableModel(Homegame homegame, CashgameSuite suite){
			ItemModels = GetItemModels(homegame, suite);
		}

		private List<LeaderboardTableItemModel> GetItemModels(Homegame homegame, CashgameSuite suite){
			var results = suite.TotalResults;
			var models = new List<LeaderboardTableItemModel>();
			var rank = 1;
			foreach(var result in results){
				models.Add(new LeaderboardTableItemModel(homegame, result, rank));
				rank++;
			}
			return models;
		}

	}

}