using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Web.Models.CashgameModels.Matrix{

	public class MatrixTableModel{

	    public bool ShowYear { get; set; }
	    public List<MatrixTableColumnHeaderModel> ColumnHeaderModels { get; set; }
	    public List<MatrixTableRowModel> RowModels { get; set; }

		public MatrixTableModel (Homegame homegame, CashgameSuite suite){
            ShowYear = SpansMultipleYears(suite.Cashgames);
            ColumnHeaderModels = GetHeaderModels(homegame, suite.Cashgames);
            RowModels = GetRowModels(homegame, suite, suite.TotalResults);
		}

		private List<MatrixTableColumnHeaderModel> GetHeaderModels(Homegame homegame, IEnumerable<Cashgame> cashgames)
		{
		    return cashgames.Select(cashgame => new MatrixTableColumnHeaderModel(homegame, cashgame, ShowYear)).ToList();
		}

	    private List<MatrixTableRowModel> GetRowModels(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results){
            var models = new List<MatrixTableRowModel>();
			var rank = 0;
			foreach(var result in results){
				rank++;
				models.Add(new MatrixTableRowModel(homegame, suite, result, rank));
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