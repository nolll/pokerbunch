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
            var canDelete = !_cashgameRepository.HasPlayed(request.PlayerId);

            if (canDelete)
            {
                _playerRepository.Delete(request.PlayerId);
            }

            return new DeletePlayerResult(canDelete, request.Slug, request.PlayerId);
        }
    }
}