using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameFactsUrlModel : HomegameYearUrlModel{

		public CashgameFactsUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameFacts, RouteFormats.CashgameFactsWithYear, homegame, year)
        {
		}

	}

}