using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Listing;

namespace Web.ModelFactories.CashgameModelFactories.Listing
{
    public class CashgameListingPageModelFactory : ICashgameListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;
        private readonly ICashgameListingTableModelFactory _cashgameListingTableModelFactory;

        public CashgameListingPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory,
            ICashgameListingTableModelFactory cashgameListingTableModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
            _cashgameListingTableModelFactory = cashgameListingTableModelFactory;
        }

        public CashgameListingPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year)
        {
            return new CashgameListingPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        ListingTableModel = _cashgameListingTableModelFactory.Create(homegame, cashgames),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "listing", years, year)
                };
        }
    }
}