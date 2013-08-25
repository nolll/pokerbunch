using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class PlayerDetailsUrlModel : PlayerUrlModel{

		public PlayerDetailsUrlModel(Homegame homegame, Player player): base(RouteFormats.PlayerDetails, homegame, player)
        {
		}

	}

}