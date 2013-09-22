using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class PlayerInviteConfirmationUrlModel : PlayerUrlModel{

		public PlayerInviteConfirmationUrlModel(Homegame homegame, Player player)
            : base(RouteFormats.PlayerInviteConfirmation, homegame, player)
        {
		}

	}

}