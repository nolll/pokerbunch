using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameLeaderboardUrlModel : HomegameYearUrlModel{

		public CashgameLeaderboardUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameLeaderboard, RouteFormats.CashgameLeaderboardWithYear, homegame, year)
        {
		}

	}

}