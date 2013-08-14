using Core.Classes;
using Web.Models;
using Web.Routing;

namespace app{

	public class RunningCashgameUrlModel : HomegameUrlModel{

		public RunningCashgameUrlModel(Homegame homegame) : base(RouteFormats.RunningCashgame, homegame){}

	}

}