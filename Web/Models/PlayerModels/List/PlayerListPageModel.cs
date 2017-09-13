using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.PlayerModels.List
{
    public class PlayerListPageModel : BunchPageModel
    {
        public IList<PlayerItemModel> PlayerModels { get; private set; }
	    public string AddUrl { get; private set; }
	    public bool ShowAddLink { get; private set; }

        public PlayerListPageModel(BunchContext.Result contextResult, PlayerList.Result playerListResult)
            : base(contextResult)
        {
            PlayerModels = playerListResult.Players.Select(item => new PlayerItemModel(item)).ToList();
            AddUrl = new AddPlayerUrl(playerListResult.Slug).Relative;
            ShowAddLink = playerListResult.CanAddPlayer;
        }

        public override string BrowserTitle => "Player List";

        public override View GetView()
        {
            return new View("~/Views/Pages/PlayerList/List.cshtml");
        }
    }
}