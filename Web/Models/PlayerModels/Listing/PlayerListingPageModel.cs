using System.Collections.Generic;
using Core.Classes;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.Listing{

	public class PlayerListingPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<PlayerItemModel> PlayerModels { get; set; }
	    public UrlModel AddUrl { get; set; }
	    public bool ShowAddLink { get; set; }

		public PlayerListingPageModel(
            User user,
			Homegame homegame,
			IList<Player> players,
			bool isInManagerMode,
			Cashgame runningGame = null)
		{
		    BrowserTitle = "Player List";
            PageProperties = new PageProperties(user, homegame, runningGame);
			PlayerModels = GetPlayerModels(homegame, players);
			AddUrl = new PlayerAddUrlModel(homegame);
			ShowAddLink = isInManagerMode;
		}

		private List<PlayerItemModel> GetPlayerModels(Homegame homegame, IList<Player> players){
			var models = new List<PlayerItemModel>();
			foreach(var player in players){
				models.Add(new PlayerItemModel(homegame, player));
			}
			return models;
		}

	}

}