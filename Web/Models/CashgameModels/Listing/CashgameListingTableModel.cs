using System.Collections.Generic;
using Core.Classes;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingTableModel{

		public bool ShowYear { get; set; }
		public List<CashgameListingTableItemModel> ListItemModels { get; set; }

		public CashgameListingTableModel(Homegame homegame, List<Cashgame> cashgames){
			ShowYear = SpansMultipleYears(cashgames);
			ListItemModels = GetListItemModels(homegame, cashgames, ShowYear);
		}

		private List<CashgameListingTableItemModel> GetListItemModels(Homegame homegame, IEnumerable<Cashgame> cashgames, bool showYear)
		{
		    var models = new List<CashgameListingTableItemModel>();
			foreach(var cashgame in cashgames){
				models.Add(new CashgameListingTableItemModel(homegame, cashgame, showYear));
			}
			return models;
		}

		private bool SpansMultipleYears(IEnumerable<Cashgame> cashgames){
			var years = new List<int>();
			foreach(var cashgame in cashgames)
			{
                if (cashgame.StartTime.HasValue)
                {
                    var year = cashgame.StartTime.Value.Year;
				    if(!years.Contains(year)){
					    years.Add(year);
				    }
                }
			}
			return years.Count > 1;
		}

	}

}