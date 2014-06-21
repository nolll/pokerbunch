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
        private readonly IBunchContextInteractor _contextInteractor;

        public PlayerListPageBuilder(
            IPlayerItemModelFactory playerItemModelFactory,
            IAuth auth,
            IPlayerListInteractor playerListInteractor,
            IBunchContextInteractor contextInteractor)
        {
            _playerItemModelFactory = playerItemModelFactory;
            _auth = auth;
            _playerListInteractor = playerListInteractor;
            _contextInteractor = contextInteractor;
        }

        public PlayerListPageModel Build(string slug)
        {
            var isInManagerMode = _auth.IsInRole(slug, Role.Manager);
            var request = new PlayerListRequest(slug);
            var result = _playerListInteractor.Execute(request);

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

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