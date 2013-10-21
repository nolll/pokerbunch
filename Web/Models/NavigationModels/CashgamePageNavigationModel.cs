using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

	public class CashgamePageNavigationModel{

	    public string Selected { get; set; }
	    public CashgameMatrixUrlModel MatrixLink { get; set; }
	    public CashgameLeaderboardUrlModel LeaderboardLink { get; set; }
	    public string ChartLink { get; set; }
	    public CashgameListingUrlModel ListingLink { get; set; }
	    public CashgameFactsUrlModel FactsLink { get; set; }

    }
}
