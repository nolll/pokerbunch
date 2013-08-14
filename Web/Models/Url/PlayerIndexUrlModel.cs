using Core.Classes;
using Web.Routing;

namespace Web.Models.Url{

	public class PlayerIndexUrlModel : HomegameUrlModel{

		public PlayerIndexUrlModel(Homegame homegame) : base(RouteFormats.PlayerIndex, homegame){}

	}

}