using Application.UseCases.BunchContext;
using Application.UseCases.PlayerList;
using Web.Models.PlayerModels.List;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerListPageBuilder : IPlayerListPageBuilder
    {
        private readonly IPlayerListInteractor _playerListInteractor;
        private readonly IBunchContextInteractor _contextInteractor;

        public PlayerListPageBuilder(
            IPlayerListInteractor playerListInteractor,
            IBunchContextInteractor contextInteractor)
        {
            _playerListInteractor = playerListInteractor;
            _contextInteractor = contextInteractor;
        }

        public PlayerListPageModel Build(string slug)
        {
            var playerListResult = _playerListInteractor.Execute(new PlayerListRequest(slug));
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            return new PlayerListPageModel(contextResult, playerListResult);
        }
    }
}