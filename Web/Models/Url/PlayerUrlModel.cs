using Core.Classes;

namespace Web.Models.Url{

	class PlayerUrlModel : UrlModel{

		public PlayerUrlModel(string format, Homegame homegame, Player player){
			var url = UrlFormatter.FormatPlayer(format, homegame, player);
			SetUrl(url);
		}

	}

}