using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeletePlayer
{
    public class DeletePlayerInteractor
    {
        public static DeletePlayerResult Execute(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository,
            DeletePlayerRequest request)
        {
            var hasPlayed = cashgameRepository.HasPlayed(request.PlayerId);

            if (!hasPlayed)
            {
                var bunch = bunchRepository.GetBySlug(request.Slug);
                var player = playerRepository.GetById(request.PlayerId);
                playerRepository.Delete(bunch, player);
            }

            var returnUrl = CreateReturnUrl(request, hasPlayed);
            return new DeletePlayerResult(returnUrl);
        }

        private static Url CreateReturnUrl(DeletePlayerRequest request, bool hasPlayed)
        {
            if (hasPlayed)
                return new PlayerDetailsUrl(request.Slug, request.PlayerId);
            return new PlayerIndexUrl(request.Slug);
        }
    }
}