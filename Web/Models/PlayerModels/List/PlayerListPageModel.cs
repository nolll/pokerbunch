using System.Collections.Generic;
using System.Linq;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerList;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.List
{
    public class PlayerListPageModel : BunchPageModel
    {
        public IList<PlayerItemModel> PlayerModels { get; private set; }
	    public string AddUrl { get; private set; }
	    public bool ShowAddLink { get; private set; }

        public PlayerListPageModel(BunchContextResult contextResult, PlayerListResult playerListResult)
            : base("Player List", contextResult)
        {
            PlayerModels = playerListResult.Players.Select(item => new PlayerItemModel(item)).ToList();
            AddUrl = playerListResult.AddUrl.Relative;
            ShowAddLink = playerListResult.CanAddPlayer;
        }
    }
}