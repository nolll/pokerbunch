using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Application.UseCases.PlayerList;
using Core.Entities;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListPageBuilder : IPlayerListPageBuilder
    {
        private readonly IPlayerItemModelFactory _playerItemModelFactory;
        private readonly IAuth _auth;
        private readonly IPlayerListInteractor _playerListInteractor;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public PlayerListPageBuilder(
            IPlayerItemModelFactory playerItemModelFactory,
            IAuth auth,
            IPlayerListInteractor playerListInteractor,
            IBunchContextInteractor bunchContextInteractor)
        {
            _playerItemModelFactory = playerItemModelFactory;
            _auth = auth;
            _playerListInteractor = playerListInteractor;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public PlayerListPageModel Build(string slug)
        {
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            var request = new PlayerListRequest {Slug = slug};
            var result = _playerListInteractor.Execute(request);

            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = slug});

            return new PlayerListPageModel
                {
                    BrowserTitle = "Player List",
                    PageProperties = new PageProperties(contextResult),
			        PlayerModels = _playerItemModelFactory.CreateList(slug, result.Players),
			        AddUrl = new AddPlayerUrl(slug),
			        ShowAddLink = isInManagerMode
                };
        }
    }
}