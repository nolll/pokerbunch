using Core.Repositories;

namespace Core.UseCases.PlayerBadges
{
    public class PlayerBadgesInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerBadgesInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new PlayerBadgesResult(request.PlayerId, cashgames);
        }
    }
}