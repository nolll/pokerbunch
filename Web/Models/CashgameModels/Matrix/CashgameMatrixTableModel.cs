using System.Collections.Generic;

namespace Web.Models.CashgameModels.Matrix{

	public class CashgameMatrixTableModel{

	    public bool ShowYear { get; set; }
	    public List<CashgameMatrixTableColumnHeaderModel> ColumnHeaderModels { get; set; }
	    public List<CashgameMatrixTableRowModel> RowModels { get; set; }

	}
}