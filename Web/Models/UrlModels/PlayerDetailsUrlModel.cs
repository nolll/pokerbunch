using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class PlayerDetailsUrlModel : PlayerUrlModel{

		public PlayerDetailsUrlModel(Homegame homegame, Player player): base(RouteFormats.PlayerDetails, homegame, player)
        {
		}

	}

}