using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories.Listing
{
    public class CashgameListingTableModelFactory : ICashgameListingTableModelFactory
    {
        private readonly ICashgameListingTableItemModelFactory _cashgameListingTableItemModelFactory;

        public CashgameListingTableModelFactory(ICashgameListingTableItemModelFactory cashgameListingTableItemModelFactory)
        {
            _cashgameListingTableItemModelFactory = cashgameListingTableItemModelFactory;
        }

        public CashgameListingTableModel Create(Homegame homegame, IList<Cashgame> cashgames)
        {
            var showYear = SpansMultipleYears(cashgames);

            return new CashgameListingTableModel
                {
                    ShowYear = showYear,
                    ListItemModels = GetListItemModels(homegame, cashgames, showYear)
                };
        }

        private List<CashgameListingTableItemModel> GetListItemModels(Homegame homegame, IEnumerable<Cashgame> cashgames, bool showYear)
        {
            var models = new List<CashgameListingTableItemModel>();
            foreach (var cashgame in cashgames)
            {
                models.Add(_cashgameListingTableItemModelFactory.Create(homegame, cashgame, showYear));
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