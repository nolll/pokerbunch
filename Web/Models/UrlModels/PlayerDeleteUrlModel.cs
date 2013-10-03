using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class PlayerDeleteUrlModel : PlayerUrlModel{

		public PlayerDeleteUrlModel(Homegame homegame, Player player) : base(RouteFormats.PlayerDelete, homegame, player){
		}

	}

}