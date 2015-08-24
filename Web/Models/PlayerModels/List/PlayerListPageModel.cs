using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.PlayerModels.List
{
    public class PlayerListPageModel : BunchPageModel
    {
        public IList<PlayerItemModel> PlayerModels { get; private set; }
	    public string AddUrl { get; private set; }
	    public bool ShowAddLink { get; private set; }

        public PlayerListPageModel(BunchContext.Result contextResult, PlayerList.Result playerListResult)
            : base("Player List", contextResult)
        {
            PlayerModels = playerListResult.Players.Select(item => new PlayerItemModel(item)).ToList();
            AddUrl = new AddPlayerUrl(playerListResult.Slug).Relative;
            ShowAddLink = playerListResult.CanAddPlayer;
        }
    }
}