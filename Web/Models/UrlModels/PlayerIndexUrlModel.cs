using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class PlayerIndexUrlModel : HomegameUrlModel{

		public PlayerIndexUrlModel(Homegame homegame) : base(RouteFormats.PlayerIndex, homegame){}

	}

}