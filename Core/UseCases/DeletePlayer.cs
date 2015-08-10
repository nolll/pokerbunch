using Core.Repositories;

namespace Core.UseCases
{
    public class DeletePlayer
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public DeletePlayer(IPlayerRepository playerRepository, ICashgameRepository cashgameRepository)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var canDelete = !_cashgameRepository.HasPlayed(request.PlayerId);

            if (canDelete)
            {
                _playerRepository.Delete(request.PlayerId);
            }

            return new Result(canDelete, request.Slug, request.PlayerId);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string slug, int playerId)
            {
                PlayerId = playerId;
                Slug = slug;
            }
        }

        public class Result
        {
            public bool Deleted { get; private set; }
            public string Slug { get; private set; }
            public int PlayerId { get; private set; }

            public Result(bool deleted, string slug, int playerId)
            {
                Deleted = deleted;
                Slug = slug;
                PlayerId = playerId;
            }
        }
    }
}