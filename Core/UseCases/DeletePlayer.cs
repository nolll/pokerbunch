using System.Linq;
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
            var cashgameCollection = _cashgameRepository.PlayerList(request.PlayerId);
            var hasPlayed = cashgameCollection.Cashgames.Any();
            var canDelete = !hasPlayed;

            if (canDelete)
            {
                _playerRepository.Delete(request.PlayerId);
            }

            return new Result(canDelete, cashgameCollection.Bunch.Id, request.PlayerId);
        }

        public class Request
        {
            public string PlayerId { get; }

            public Request(string playerId)
            {
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public bool Deleted { get; private set; }
            public string Slug { get; private set; }
            public string PlayerId { get; private set; }

            public Result(bool deleted, string slug, string playerId)
            {
                Deleted = deleted;
                Slug = slug;
                PlayerId = playerId;
            }
        }
    }
}