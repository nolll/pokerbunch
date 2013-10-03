using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class CashgamePublishUrlModel : CashgameUrlModel{

		public CashgamePublishUrlModel(Homegame homegame, Cashgame cashgame)
			: base(RouteFormats.CashgamePublish, homegame, cashgame)
        {
		}

	}

}