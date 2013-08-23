using Core.Classes;
using Web.Routing;
using app;

namespace Web.Models.Url{

	class PlayerDetailsUrlModel : PlayerUrlModel{

		public PlayerDetailsUrlModel(Homegame homegame, Player player): base(RouteFormats.PlayerDetails, homegame, player)
        {
		}

	}

}