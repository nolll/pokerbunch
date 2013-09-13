using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
        public CashgameListingTableModel ListingTableModel { get; set; }
        public CashgameNavigationModel CashgameNavModel { get; set; }

		public CashgameListingPageModel(User user,
									Homegame homegame,
									List<Cashgame> cashgames,
									List<int>  years,
									int? year,
									Cashgame runningGame)
		{
            BrowserTitle = "Cashgame List";
            PageProperties = new PageProperties(user, homegame, runningGame);
			ListingTableModel = new CashgameListingTableModel(homegame, cashgames);
			CashgameNavModel = new CashgameNavigationModel(homegame, "listing", years, year, runningGame);
		}

	}

}