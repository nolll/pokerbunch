using Core.Classes;
using Web.Models;
using Web.Routing;

namespace app{

	public class CashgameIndexUrlModel : HomegameUrlModel{

		public CashgameIndexUrlModel(Homegame homegame) : base(RouteFormats.CashgameIndex, homegame){}

	}

}