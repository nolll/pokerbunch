using System.Collections.Generic;
using System.Linq;
using Web.Extensions;

namespace Web.Models.CashgameModels.Matrix
{
	public class CashgameMatrixTableModel : IViewModel
    {
	    public List<CashgameMatrixTableColumnHeaderModel> ColumnHeaderModels { get; }
	    public List<CashgameMatrixTableRowModel> RowModels { get; }

        public CashgameMatrixTableModel(Core.UseCases.Matrix.Result matrixResult)
        {
            var showYear = matrixResult.SpansMultipleYears;
            var headerModels = matrixResult.GameItems.Select(o => new CashgameMatrixTableColumnHeaderModel(o, showYear)).ToList();

            ColumnHeaderModels = headerModels;
            RowModels = matrixResult.PlayerItems.Select(playerItem => new CashgameMatrixTableRowModel(matrixResult.GameItems, playerItem)).ToList();
        }

	    public View GetView()
	    {
	        return new View("~/Views/Pages/Matrix/MatrixTable.cshtml");
	    }
    }
}