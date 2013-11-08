using System.Collections.Generic;
using System.Linq;
using Core.Classes;
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
            var showYear = SpansMultipleYears(suite.Cashgames);

            return new CashgameMatrixTableModel
                {
                    ShowYear = showYear,
                    ColumnHeaderModels = GetHeaderModels(homegame, suite.Cashgames, showYear),
                    RowModels = GetRowModels(homegame, suite, suite.TotalResults)
                };
        }

        private List<CashgameMatrixTableColumnHeaderModel> GetHeaderModels(Homegame homegame, IEnumerable<Cashgame> cashgames, bool showYear)
        {
            return cashgames.Select(cashgame => _cashgameMatrixTableColumnHeaderModelFactory.Create(homegame, cashgame, showYear)).ToList();
        }

        private List<CashgameMatrixTableRowModel> GetRowModels(Homegame homegame, CashgameSuite suite, IEnumerable<CashgameTotalResult> results)
        {
            var models = new List<CashgameMatrixTableRowModel>();
            var rank = 0;
            foreach (var result in results)
            {
                rank++;
                models.Add(_cashgameMatrixTableRowModelFactory.Create(homegame, suite, result, rank));
            }
            return models;
        }

        private bool SpansMultipleYears(IEnumerable<Cashgame> cashgames)
        {
            var years = new List<int>();
            foreach (var cashgame in cashgames)
            {
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