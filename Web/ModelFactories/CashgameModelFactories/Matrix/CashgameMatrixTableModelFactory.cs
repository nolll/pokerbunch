using System.Linq;
using Core.Entities;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableModelFactory : ICashgameMatrixTableModelFactory
    {
        private readonly ICashgameMatrixTableColumnHeaderModelFactory _cashgameMatrixTableColumnHeaderModelFactory;
        private readonly ICashgameMatrixTableRowModelFactory _cashgameMatrixTableRowModelFactory;

        public CashgameMatrixTableModelFactory(
            ICashgameMatrixTableColumnHeaderModelFactory cashgameMatrixTableColumnHeaderModelFactory,
            ICashgameMatrixTableRowModelFactory cashgameMatrixTableRowModelFactory)
        {
            _cashgameMatrixTableColumnHeaderModelFactory = cashgameMatrixTableColumnHeaderModelFactory;
            _cashgameMatrixTableRowModelFactory = cashgameMatrixTableRowModelFactory;
        }

        public CashgameMatrixTableModel Create(Homegame homegame, CashgameSuite suite)
        {
            var showYear = suite.SpansMultipleYears();
            var headerModels = suite.Cashgames.Select(cashgame => _cashgameMatrixTableColumnHeaderModelFactory.Create(homegame, cashgame, showYear)).ToList();

            return new CashgameMatrixTableModel
                {
                    ShowYear = showYear,
                    ColumnHeaderModels = headerModels,
                    RowModels = _cashgameMatrixTableRowModelFactory.CreateList(homegame, suite, suite.TotalResults)
                };
        }
    }
}