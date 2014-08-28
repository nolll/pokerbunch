using Core.Repositories;

namespace Application.UseCases.PlayerFacts
{
    public class PlayerFactsInteractor : IPlayerFactsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerFactsInteractor(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerFactsResult Execute(PlayerFactsRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetPublished(homegame);

            return new PlayerFactsResult(cashgames, request.PlayerId, homegame.Currency);
        }
    }
}