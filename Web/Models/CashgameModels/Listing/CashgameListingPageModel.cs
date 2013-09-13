using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingPageModel : PageProperties {

		public CashgameListingTableModel ListingTableModel;
		public CashgameNavigationModel CashgameNavModel;

		public CashgameListingPageModel(User user,
									Homegame homegame,
									List<Cashgame> cashgames,
									List<int>  years,
									int? year,
									Cashgame runningGame)
            : base(user, homegame, runningGame)
        {
			ListingTableModel = new CashgameListingTableModel(homegame, cashgames);
			CashgameNavModel = new CashgameNavigationModel(homegame, "listing", years, year, runningGame);
		}

        public override string BrowserTitle
        {
            get { return "Cashgame List"; }
        }

	}

}