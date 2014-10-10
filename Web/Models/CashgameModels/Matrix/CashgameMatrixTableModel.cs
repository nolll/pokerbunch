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
            RowModels = CreateRows(matrixResult);
        }

	    private static List<CashgameMatrixTableRowModel> CreateRows(MatrixResult matrixResult)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            foreach (var playerItem in matrixResult.PlayerItems)
            {
                models.Add(new CashgameMatrixTableRowModel(matrixResult.GameItems, playerItem));
            }
            return models;
        }
    }
}