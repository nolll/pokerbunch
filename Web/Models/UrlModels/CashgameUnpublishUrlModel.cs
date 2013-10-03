using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class CashgameUnpublishUrlModel : CashgameUrlModel{

		public CashgameUnpublishUrlModel(Homegame homegame, Cashgame cashgame)
            : base(RouteFormats.CashgameUnpublish, homegame, cashgame)
        {
		}

	}

}