using Core.Repositories;

namespace Core.UseCases.PlayerFacts
{
    public static class PlayerFactsInteractor
    {
        public static PlayerFactsResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            PlayerFactsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch);

            return new PlayerFactsResult(cashgames, request.PlayerId, bunch.Currency);
        }
    }
}