using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameEditUrlModel : CashgameUrlModel{

		public CashgameEditUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameEdit, homegame, cashgame)
        {
		}

	}

}