using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Web.Models{

	public class TableModel{

	    public bool ShowYear { get; set; }
	    public List<ColumnHeaderModel> ColumnHeaderModels { get; set; }
	    public List<RowModel> RowModels { get; set; }

		public TableModel (Homegame homegame, CashgameSuite suite){
			var results = suite.TotalResults;
			var cashgames = suite.Cashgames;
			ShowYear = SpansMultipleYears(cashgames);
			ColumnHeaderModels = GetHeaderModels(homegame, cashgames);
            RowModels = GetRowModels(homegame, suite, results);
		}

		private List<ColumnHeaderModel> GetHeaderModels(Homegame homegame, IEnumerable<Cashgame> cashgames)
		{
		    return cashgames.Select(cashgame => new ColumnHeaderModel(homegame, cashgame, ShowYear)).ToList();
		}

	    private List<RowModel> GetRowModels(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results){
            var models = new List<RowModel>();
			var rank = 0;
			foreach(var result in results){
				rank++;
				models.Add(new RowModel(homegame, suite, result, rank));
			}
			return models;
		}

		private bool SpansMultipleYears(IEnumerable<Cashgame> cashgames)
		{
		    var years = new List<int>();
			foreach(var cashgame in cashgames){
                if (cashgame.StartTime.HasValue)
                {
                    var year = cashgame.StartTime.Value.Year;
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
			}
			return years.Count > 1;
		}

	}

}