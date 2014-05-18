using Application.Services;
using Application.UseCases.PlayerList;
using Core.Entities;
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
        private readonly IAuth _auth;
        private readonly IPlayerListInteractor _playerListInteractor;

        public PlayerListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            IPlayerItemModelFactory playerItemModelFactory,
            IHomegameRepository homegameRepository,
            IAuth auth,
            IPlayerListInteractor playerListInteractor)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _playerItemModelFactory = playerItemModelFactory;
            _homegameRepository = homegameRepository;
            _auth = auth;
            _playerListInteractor = playerListInteractor;
        }

        public PlayerListPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            var request = new PlayerListRequest {Slug = slug};
            var result = _playerListInteractor.Execute(request);

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