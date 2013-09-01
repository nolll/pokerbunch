using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class PlayerInviteUrlModel : PlayerUrlModel{

		public PlayerInviteUrlModel(Homegame homegame, Player player) : base(RouteFormats.PlayerInvite, homegame, player)
        {
		}

	}

}