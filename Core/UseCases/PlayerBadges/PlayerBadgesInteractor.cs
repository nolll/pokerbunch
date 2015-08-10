using Core.Repositories;

namespace Core.UseCases.PlayerBadges
{
    public class PlayerBadgesInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerBadgesInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchRepository.GetById(player.BunchId);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new PlayerBadgesResult(player.Id, cashgames);
        }
    }
}