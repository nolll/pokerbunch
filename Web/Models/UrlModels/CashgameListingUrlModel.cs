using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameListingUrlModel : HomegameYearUrlModel{

		public CashgameListingUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameListing, RouteFormats.CashgameListingWithYear, homegame, year)
        {
		}

	}

}