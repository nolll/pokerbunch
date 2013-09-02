using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameActionUrlModel : CashgamePlayerUrlModel{

		public CashgameActionUrlModel(Homegame homegame, Cashgame cashgame, Player player)
            : base(RouteFormats.CashgameAction, homegame, cashgame, player)
        {
		}

	}

}