using Core.Classes;
using Web.Routing;

namespace app{

	class CashgameDetailsUrlModel : CashgameUrlModel{

        public CashgameDetailsUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameDetails, homegame, cashgame)
        {
		}

	}

}