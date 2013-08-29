using Core.Classes;
using Web.Models.UrlModels;
using Web.Routing;

namespace app{

	class PlayerDeleteUrlModel : PlayerUrlModel{

		public PlayerDeleteUrlModel(Homegame homegame, Player player) : base(RouteFormats.PlayerDelete, homegame, player){
		}

	}

}