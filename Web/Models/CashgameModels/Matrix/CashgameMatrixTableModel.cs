using System.Collections.Generic;
using System.Linq;
using Application.UseCases.Matrix;
using Core.Entities;

namespace Web.Models.CashgameModels.Matrix
{
	public class CashgameMatrixTableModel
    {
	    public List<CashgameMatrixTableColumnHeaderModel> ColumnHeaderModels { get; private set; }
	    public List<CashgameMatrixTableRowModel> RowModels { get; private set; }

        public CashgameMatrixTableModel(MatrixResult matrixResult, Bunch bunch, CashgameSuite suite)
        {
            var showYear = suite.SpansMultipleYears;
            var headerModels = matrixResult.GameItems.Select(o => new CashgameMatrixTableColumnHeaderModel(o, showYear)).ToList();

            ColumnHeaderModels = headerModels;
            RowModels = CreateRows(bunch, suite, suite.TotalResults);
        }

	    private static List<CashgameMatrixTableRowModel> CreateRows(Bunch bunch, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            var rank = 0;
            foreach (var result in results)
            {
                rank++;
                models.Add(new CashgameMatrixTableRowModel(bunch, suite, result.Player, result, rank));
            }
            return models;
        }
    }
}