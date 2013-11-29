using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Services;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.Listing;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListingPageModelFactory : IPlayerListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IPlayerItemModelFactory _playerItemModelFactory;

        public PlayerListingPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IPlayerItemModelFactory playerItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _playerItemModelFactory = playerItemModelFactory;
        }

        public PlayerListingPageModel Create(User user, Homegame homegame, IList<Player> players, bool isInManagerMode)
        {
            return new PlayerListingPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
			        PlayerModels = GetPlayerModels(homegame, players),
			        AddUrl = _urlProvider.GetPlayerAddUrl(homegame),
			        ShowAddLink = isInManagerMode
                };
        }

        private List<PlayerItemModel> GetPlayerModels(Homegame homegame, IEnumerable<Player> players)
        {
            return players.Select(player => _playerItemModelFactory.Create(homegame, player)).ToList();
        }
    }
}