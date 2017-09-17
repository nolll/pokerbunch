using System.Linq;
using Core.Services;

namespace Core.UseCases
{
    public class DeletePlayer
    {
        private readonly IPlayerService _playerService;
        private readonly ICashgameService _cashgameService;

        public DeletePlayer(IPlayerService playerService, ICashgameService cashgameService)
        {
            _playerService = playerService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var cashgame = _cashgameService.PlayerList(request.PlayerId);
            var hasPlayed = cashgame.Any();
            var canDelete = !hasPlayed;

            if (canDelete)
            {
                _playerService.Delete(request.PlayerId);
            }

            return new Result(canDelete, player.BunchId, request.PlayerId);
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
            public bool Deleted { get; }
            public string Slug { get; }
            public string PlayerId { get; }

            public Result(bool deleted, string slug, string playerId)
            {
                Deleted = deleted;
                Slug = slug;
                PlayerId = playerId;
            }
        }
    }
}