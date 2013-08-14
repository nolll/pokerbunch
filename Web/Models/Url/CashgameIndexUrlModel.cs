using Core.Classes;
using Web.Routing;

namespace Web.Models.Url{

	public class CashgameIndexUrlModel : HomegameUrlModel{

		public CashgameIndexUrlModel(Homegame homegame) : base(RouteFormats.CashgameIndex, homegame){}

	}

}