using Application.UseCases.BunchContext;
using Application.UseCases.PlayerDetails;
using Core.Repositories;
using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageBuilder : IPlayerDetailsPageBuilder
    {
        private readonly IPlayerFactsModelFactory _playerFactsModelFactory;
        private readonly IPlayerBadgesModelFactory _playerBadgesModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly IPlayerDetailsInteractor _playerDetailsInteractor;

        public PlayerDetailsPageBuilder(
            IPlayerFactsModelFactory playerFactsModelFactory,
            IPlayerBadgesModelFactory playerBadgesModelFactory,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor contextInteractor,
            IPlayerDetailsInteractor playerDetailsInteractor)
        {
            _playerFactsModelFactory = playerFactsModelFactory;
            _playerBadgesModelFactory = playerBadgesModelFactory;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _contextInteractor = contextInteractor;
            _playerDetailsInteractor = playerDetailsInteractor;
        }

        public PlayerDetailsPageModel Build(string slug, int playerId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            var playerDetailsResult = _playerDetailsInteractor.Execute(new PlayerDetailsRequest(slug, playerId));

            return new PlayerDetailsPageModel(contextResult, playerDetailsResult)
                {
                    PlayerFactsModel = _playerFactsModelFactory.Create(homegame.Currency, cashgames, player),
                    PlayerBadgesModel = _playerBadgesModelFactory.Create(player.Id, cashgames)
                };
        }
    }
}