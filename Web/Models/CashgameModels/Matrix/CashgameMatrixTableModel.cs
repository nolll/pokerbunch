using System.Collections.Generic;
using System.Linq;
using Core.UseCases.Matrix;

namespace Web.Models.CashgameModels.Matrix
{
	public class CashgameMatrixTableModel
    {
	    public List<CashgameMatrixTableColumnHeaderModel> ColumnHeaderModels { get; private set; }
	    public List<CashgameMatrixTableRowModel> RowModels { get; private set; }

        public CashgameMatrixTableModel(MatrixResult matrixResult)
        {
            var showYear = matrixResult.SpansMultipleYears;
            var headerModels = matrixResult.GameItems.Select(o => new CashgameMatrixTableColumnHeaderModel(o, showYear)).ToList();

            ColumnHeaderModels = headerModels;
            RowModels = matrixResult.PlayerItems.Select(playerItem => new CashgameMatrixTableRowModel(matrixResult.GameItems, playerItem)).ToList();
        }
    }
}