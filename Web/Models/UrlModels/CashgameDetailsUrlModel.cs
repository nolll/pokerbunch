using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameDetailsUrlModel : CashgameUrlModel{

        public CashgameDetailsUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameDetails, homegame, cashgame)
        {
		}

	}

}