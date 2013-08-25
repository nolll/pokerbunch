using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{

	class PlayerUrlModel : UrlModel{

		public PlayerUrlModel(string format, Homegame homegame, Player player){
			var url = UrlFormatter.FormatPlayer(format, homegame, player);
			SetUrl(url);
		}

	}

}