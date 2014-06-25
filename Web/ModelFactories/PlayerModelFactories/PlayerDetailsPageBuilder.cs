using Application.UseCases.BunchContext;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageBuilder : IPlayerDetailsPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly IPlayerDetailsInteractor _playerDetailsInteractor;
        private readonly IPlayerFactsInteractor _playerFactsInteractor;
        private readonly IPlayerBadgesInteractor _playerBadgesInteractor;

        public PlayerDetailsPageBuilder(
            IBunchContextInteractor contextInteractor,
            IPlayerDetailsInteractor playerDetailsInteractor,
            IPlayerFactsInteractor playerFactsInteractor,
            IPlayerBadgesInteractor playerBadgesInteractor)
        {
            _contextInteractor = contextInteractor;
            _playerDetailsInteractor = playerDetailsInteractor;
            _playerFactsInteractor = playerFactsInteractor;
            _playerBadgesInteractor = playerBadgesInteractor;
        }

        public PlayerDetailsPageModel Build(string slug, int playerId)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            var detailsResult = _playerDetailsInteractor.Execute(new PlayerDetailsRequest(slug, playerId));
            var factsResult = _playerFactsInteractor.Execute(new PlayerFactsRequest(slug, playerId));
            var badgesResult = _playerBadgesInteractor.Execute(new PlayerBadgesRequest(slug, playerId));

            return new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
        }
    }
}