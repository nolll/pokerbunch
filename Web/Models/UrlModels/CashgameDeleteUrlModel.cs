using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameDeleteUrlModel : CashgameUrlModel{

		public CashgameDeleteUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameDelete, homegame, cashgame)
        {
		}

	}

}