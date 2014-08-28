using Core.Repositories;

namespace Application.UseCases.PlayerBadges
{
    public class PlayerBadgesInteractor : IPlayerBadgesInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerBadgesInteractor(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetPublished(homegame);

            return new PlayerBadgesResult(request.PlayerId, cashgames);
        }
    }
}