using Core.Repositories;

namespace Application.UseCases.PlayerBadges
{
    public class PlayerBadgesInteractor : IPlayerBadgesInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerBadgesInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerBadgesResult Execute(PlayerBadgesRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetPublished(homegame);

            return new PlayerBadgesResult(request.PlayerId, cashgames);
        }
    }
}