using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

	public class CashgamePageNavigationModel{

	    public string Selected { get; set; }
	    public CashgameMatrixUrlModel MatrixLink { get; set; }
	    public CashgameLeaderboardUrlModel LeaderboardLink { get; set; }
	    public CashgameChartUrlModel ChartLink { get; set; }
	    public CashgameListingUrlModel ListingLink { get; set; }
	    public CashgameFactsUrlModel FactsLink { get; set; }

		public CashgamePageNavigationModel(Homegame homegame, int? year = null, string view = null, Cashgame runningGame = null)
		{
		    Selected = view;
            MatrixLink = new CashgameMatrixUrlModel(homegame, year);
			LeaderboardLink = new CashgameLeaderboardUrlModel(homegame, year);
			ChartLink = new CashgameChartUrlModel(homegame, year);
			ListingLink = new CashgameListingUrlModel(homegame, year);
			FactsLink = new CashgameFactsUrlModel(homegame, year);
		}

	}

}
