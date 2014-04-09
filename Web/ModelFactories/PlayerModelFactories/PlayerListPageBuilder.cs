using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.PlayerModels.List;
using Web.Security;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListPageBuilder : IPlayerListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly IPlayerItemModelFactory _playerItemModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public PlayerListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IPlayerItemModelFactory playerItemModelFactory,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            IAuth auth)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _playerItemModelFactory = playerItemModelFactory;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public PlayerListPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            var players = _playerRepository.GetList(homegame).OrderBy(o => o.DisplayName).ToList();

            return new PlayerListPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        PlayerModels = GetPlayerModels(homegame, players),
			        AddUrl = _urlProvider.GetPlayerAddUrl(slug),
			        ShowAddLink = isInManagerMode
                };
        }

        private List<PlayerItemModel> GetPlayerModels(Homegame homegame, IEnumerable<Player> players)
        {
            return players.Select(player => _playerItemModelFactory.Create(homegame, player)).ToList();
        }
    }
}