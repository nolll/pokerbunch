using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Listing;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameListingPageModelFactory : ICashgameListingPageModelFactory
    {
        public CashgameListingPageModel Create(User user, Homegame homegame, List<Cashgame> cashgames, List<int> years, int? year, Cashgame runningGame)
        {
            return new CashgameListingPageModel
                {
                    BrowserTitle = "Cashgame List",
                    PageProperties = new PageProperties(user, homegame, runningGame),
			        ListingTableModel = new CashgameListingTableModel(homegame, cashgames),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "listing", years, year, runningGame)
                };
        }
    }
}