using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Listing{

	public class CashgameListingPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
        public CashgameListingTableModel ListingTableModel { get; set; }
        public CashgameNavigationModel CashgameNavModel { get; set; }
	}

}