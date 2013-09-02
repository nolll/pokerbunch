using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameDetailsChartJsonUrlModel : CashgameUrlModel{

		public CashgameDetailsChartJsonUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameDetailsChartJson, homegame, cashgame)
        {
		}

	}

}