using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class RunningCashgameUrlModel : HomegameUrlModel{

		public RunningCashgameUrlModel(Homegame homegame) : base(RouteFormats.RunningCashgame, homegame){}

	}

}