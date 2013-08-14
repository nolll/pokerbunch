using Core.Classes;
using Web.Models;
using Web.Routing;

namespace app{

	public class PlayerIndexUrlModel : HomegameUrlModel{

		public PlayerIndexUrlModel(Homegame homegame) : base(RouteFormats.PlayerIndex, homegame){}

	}

}