using Core.Classes;
using Web.Routing;

namespace Web.Models.Url{

	public class CashgameAddUrlModel : HomegameUrlModel{

		public CashgameAddUrlModel(Homegame homegame) : base(RouteFormats.CashgameAdd, homegame){}

	}

}