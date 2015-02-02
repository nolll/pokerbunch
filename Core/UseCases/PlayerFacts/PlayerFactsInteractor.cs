using Core.Repositories;

namespace Core.UseCases.PlayerFacts
{
    public class PlayerFactsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerFactsInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public PlayerFactsResult Execute(PlayerFactsRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id);

            return new PlayerFactsResult(cashgames, request.PlayerId, bunch.Currency);
        }
    }
}