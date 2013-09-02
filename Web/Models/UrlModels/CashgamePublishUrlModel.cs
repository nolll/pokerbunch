using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgamePublishUrlModel : CashgameUrlModel{

		public CashgamePublishUrlModel(Homegame homegame, Cashgame cashgame)
			: base(RouteFormats.CashgamePublish, homegame, cashgame)
        {
		}

	}

}