using System.Collections.Generic;
using System.Linq;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerList;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.List
{
    public class PlayerListPageModel : BunchPageModel
    {
        public IList<PlayerItemModel> PlayerModels { get; private set; }
	    public Url AddUrl { get; private set; }
	    public bool ShowAddLink { get; private set; }

        public PlayerListPageModel(BunchContextResult contextResult, PlayerListResult playerListResult)
            : base("Player List", contextResult)
        {
            PlayerModels = playerListResult.Players.Select(item => new PlayerItemModel(playerListResult.Slug, item)).ToList();
            AddUrl = new AddPlayerUrl(playerListResult.Slug);
            ShowAddLink = playerListResult.CanAddPlayer;
        }
    }
}