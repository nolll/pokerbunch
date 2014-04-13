using Application.Services;
using Core.Classes;
using Core.Repositories;
using Core.UseCases.ShowPlayerList;
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
        private readonly IAuth _auth;
        private readonly IShowPlayerListInteractor _showPlayerListInteractor;

        public PlayerListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IPlayerItemModelFactory playerItemModelFactory,
            IHomegameRepository homegameRepository,
            IAuth auth,
            IShowPlayerListInteractor showPlayerListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _playerItemModelFactory = playerItemModelFactory;
            _homegameRepository = homegameRepository;
            _auth = auth;
            _showPlayerListInteractor = showPlayerListInteractor;
        }

        public PlayerListPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            var result = _showPlayerListInteractor.Execute(slug);

            return new PlayerListPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        PlayerModels = _playerItemModelFactory.CreateList(slug, result.Players),
			        AddUrl = _urlProvider.GetPlayerAddUrl(slug),
			        ShowAddLink = isInManagerMode
                };
        }
    }
}