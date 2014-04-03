using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListPageModelFactory : IPlayerListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IPlayerItemModelFactory _playerItemModelFactory;

        public PlayerListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IPlayerItemModelFactory playerItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _playerItemModelFactory = playerItemModelFactory;
        }

        public PlayerListPageModel Create(Homegame homegame, IList<Player> players, bool isInManagerMode)
        {
            return new PlayerListPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        PlayerModels = GetPlayerModels(homegame, players),
			        AddUrl = _urlProvider.GetPlayerAddUrl(homegame.Slug),
			        ShowAddLink = isInManagerMode
                };
        }

        private List<PlayerItemModel> GetPlayerModels(Homegame homegame, IEnumerable<Player> players)
        {
            return players.Select(player => _playerItemModelFactory.Create(homegame, player)).ToList();
        }
    }
}