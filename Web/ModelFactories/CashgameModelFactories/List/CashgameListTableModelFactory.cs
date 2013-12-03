using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.List;

namespace Web.ModelFactories.CashgameModelFactories.List
{
    public class CashgameListTableModelFactory : ICashgameListTableModelFactory
    {
        private readonly ICashgameListTableItemModelFactory _cashgameListTableItemModelFactory;

        public CashgameListTableModelFactory(ICashgameListTableItemModelFactory cashgameListTableItemModelFactory)
        {
            _cashgameListTableItemModelFactory = cashgameListTableItemModelFactory;
        }

        public CashgameListTableModel Create(Homegame homegame, IList<Cashgame> cashgames)
        {
            var showYear = SpansMultipleYears(cashgames);

            return new CashgameListTableModel
                {
                    ShowYear = showYear,
                    ListItemModels = GetListItemModels(homegame, cashgames, showYear)
                };
        }

        private List<CashgameListTableItemModel> GetListItemModels(Homegame homegame, IEnumerable<Cashgame> cashgames, bool showYear)
        {
            var models = new List<CashgameListTableItemModel>();
            foreach (var cashgame in cashgames)
            {
                models.Add(_cashgameListTableItemModelFactory.Create(homegame, cashgame, showYear));
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