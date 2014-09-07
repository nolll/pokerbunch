using Core.Repositories;

namespace Application.UseCases.PlayerFacts
{
    public static class PlayerFactsInteractor
    {
        public static PlayerFactsResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            PlayerFactsRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(homegame);

            return new PlayerFactsResult(cashgames, request.PlayerId, homegame.Currency);
        }
    }
}