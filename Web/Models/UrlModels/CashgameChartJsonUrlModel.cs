using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameChartJsonUrlModel : HomegameYearUrlModel{

		public CashgameChartJsonUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, homegame, year)
        {
		}

	}

}