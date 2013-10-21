using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Listing;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameListingPageModelFactory : ICashgameListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameNavigationModelFactory _cashgameNavigationModelFactory;

        public CashgameListingPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameNavigationModelFactory cashgameNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameNavigationModelFactory = cashgameNavigationModelFactory;
        }

        public CashgameListingPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameListingPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        ListingTableModel = new CashgameListingTableModel(homegame, cashgames),
			        CashgameNavModel = _cashgameNavigationModelFactory.Create(homegame, "listing", years, year, runningGame)
                };
        }
    }
}