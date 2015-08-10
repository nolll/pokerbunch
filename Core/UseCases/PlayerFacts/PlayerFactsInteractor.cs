using Core.Repositories;

namespace Core.UseCases.PlayerFacts
{
    public class PlayerFactsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerFactsInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public PlayerFactsResult Execute(PlayerFactsRequest request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new PlayerFactsResult(cashgames, player.Id, bunch.Currency);
        }
    }
}