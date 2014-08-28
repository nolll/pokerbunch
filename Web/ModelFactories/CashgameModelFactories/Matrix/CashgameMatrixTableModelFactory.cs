using System.Linq;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public static class CashgameMatrixTableModelFactory
    {
        public static CashgameMatrixTableModel Create(Bunch bunch, CashgameSuite suite)
        {
            var showYear = suite.SpansMultipleYears;
            var headerModels = suite.Cashgames.Select(cashgame => CashgameMatrixTableColumnHeaderModelFactory.Create(bunch, cashgame, showYear)).ToList();

            return new CashgameMatrixTableModel
                {
                    ShowYear = showYear,
                    ColumnHeaderModels = headerModels,
                    RowModels = CashgameMatrixTableRowModelFactory.CreateList(bunch, suite, suite.TotalResults)
                };
        }
    }
}