using Core.Repositories;

namespace Application.UseCases.PlayerFacts
{
    public class PlayerFactsInteractor : IPlayerFactsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerFactsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerFactsResult Execute(PlayerFactsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetPublished(homegame);

            return new PlayerFactsResult(cashgames, request.PlayerId, homegame.Currency);
        }
    }
}