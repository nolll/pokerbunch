using Core.Repositories;

namespace Application.UseCases.PlayerBadges
{
    public static class PlayerBadgesInteractor
    {
        public static PlayerBadgesResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            PlayerBadgesRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(homegame);

            return new PlayerBadgesResult(request.PlayerId, cashgames);
        }
    }
}