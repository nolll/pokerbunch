using Core.Classes;
using Web.Models;
using Web.Routing;

namespace app{

	public class CashgameAddUrlModel : HomegameUrlModel{

		public CashgameAddUrlModel(Homegame homegame) : base(RouteFormats.CashgameAdd, homegame){}

	}

}