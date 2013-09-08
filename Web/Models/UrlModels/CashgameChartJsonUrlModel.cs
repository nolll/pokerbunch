using Core.Classes;
using Web.Models.UrlModels;
using Web.Routing;

namespace app{

	public class CashgameChartJsonUrlModel : HomegameYearUrlModel{

		public CashgameChartJsonUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameChartJson, RouteFormats.CashgameChartJsonWithYear, homegame, year)
        {
		}

	}

}