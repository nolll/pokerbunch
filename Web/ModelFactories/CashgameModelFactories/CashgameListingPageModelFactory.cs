using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Listing;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameListingPageModelFactory : ICashgameListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public CashgameListingPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public CashgameListingPageModel Create(User user, Homegame homegame, IList<Cashgame> cashgames, IList<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameListingPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        ListingTableModel = new CashgameListingTableModel(homegame, cashgames),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "listing", years, year, runningGame)
                };
        }
    }
}