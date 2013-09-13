using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameActionChartJsonUrlModel : CashgamePlayerUrlModel{

		public CashgameActionChartJsonUrlModel(Homegame homegame, Cashgame cashgame, Player player)
            : base(RouteFormats.CashgameActionChartJson, homegame, cashgame, player)
        {
		}

	}

}