using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameEndUrlModel : HomegameUrlModel{

		public CashgameEndUrlModel(Homegame homegame)
            : base(RouteFormats.CashgameEnd, homegame)
        {
		}

	}

}