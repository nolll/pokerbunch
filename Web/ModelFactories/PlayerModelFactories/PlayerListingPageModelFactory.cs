using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Listing;
using Web.Models.UrlModels;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListingPageModelFactory : IPlayerListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public PlayerListingPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public PlayerListingPageModel Create(User user, Homegame homegame, List<Player> players, bool isInManagerMode, Cashgame runningGame)
        {
            return new PlayerListingPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        PlayerModels = GetPlayerModels(homegame, players),
			        AddUrl = new PlayerAddUrlModel(homegame),
			        ShowAddLink = isInManagerMode
                };
        }

        private List<PlayerItemModel> GetPlayerModels(Homegame homegame, IEnumerable<Player> players)
        {
            return players.Select(player => new PlayerItemModel(homegame, player)).ToList();
        }
    }
}