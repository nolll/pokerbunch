using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeletePlayer
{
    public class DeletePlayerInteractor
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public DeletePlayerInteractor(IPlayerRepository playerRepository, ICashgameRepository cashgameRepository)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public DeletePlayerResult Execute(DeletePlayerRequest request)
        {
            var hasPlayed = _cashgameRepository.HasPlayed(request.PlayerId);

            if (!hasPlayed)
            {
                _playerRepository.Delete(request.PlayerId);
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