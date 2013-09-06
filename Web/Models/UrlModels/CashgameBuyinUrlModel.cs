using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameBuyinUrlModel : PlayerUrlModel{

		public CashgameBuyinUrlModel(Homegame homegame, Player player)
            : base(RouteFormats.CashgameBuyin, homegame, player)
        {
		}

	}

}