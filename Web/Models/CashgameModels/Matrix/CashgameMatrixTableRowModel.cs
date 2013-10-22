using System.Collections.Generic;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixTableRowModel{

	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public string PlayerUrl { get; set; }
        public List<CashgameMatrixTableCellModel> CellModels { get; set; }
		
	}
}