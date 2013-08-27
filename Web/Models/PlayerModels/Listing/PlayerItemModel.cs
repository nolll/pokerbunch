using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.Listing{

	public class PlayerItemModel{

	    public string Name { get; set; }
        public UrlModel UrlModel { get; set; }
	    public string Email { get; set; }

		public PlayerItemModel(Homegame homegame, Player player){
			Name = player.DisplayName;
			UrlModel = new PlayerDetailsUrlModel(homegame, player);
		}

	}

}