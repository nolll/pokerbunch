using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameUnpublishUrlModel : CashgameUrlModel{

		public CashgameUnpublishUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameUnpublish, homegame, cashgame)
        {
		}

	}

}