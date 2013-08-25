using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameIndexUrlModel : HomegameUrlModel{

		public CashgameIndexUrlModel(Homegame homegame) : base(RouteFormats.CashgameIndex, homegame){}

	}

}