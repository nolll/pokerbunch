using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class CashgameChartUrlModel : HomegameYearUrlModel{

		public CashgameChartUrlModel(Homegame homegame, int? year)
            : base(RouteFormats.CashgameChart, RouteFormats.CashgameChartWithYear, homegame, year)
        {
		}

	}

}