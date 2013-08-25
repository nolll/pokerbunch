using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameAddUrlModel : HomegameUrlModel{

		public CashgameAddUrlModel(Homegame homegame) : base(RouteFormats.CashgameAdd, homegame){}

	}

}