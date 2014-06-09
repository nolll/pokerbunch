using System.Collections.Generic;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Matrix
{
    public class CashgameMatrixTableRowModel
    {
	    public int Rank { get; set; }
	    public string Name { get; set; }
	    public string UrlEncodedName { get; set; }
	    public string TotalResult { get; set; }
	    public string ResultClass { get; set; }
	    public UrlModel PlayerUrl { get; set; }
        public IList<CashgameMatrixTableCellModel> CellModels { get; set; }
	}
}