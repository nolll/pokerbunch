using Core.Services;

namespace Core.UseCases
{
    public class DeletePlayer
    {
        private readonly PlayerService _playerService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly BunchService _bunchService;

        public DeletePlayer(PlayerService playerService, CashgameService cashgameService, UserService userService, BunchService bunchService)
        {
            _playerService = playerService;
            _cashgameService = cashgameService;
            _userService = userService;
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var bunch = _bunchService.Get(player.Slug);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUserId(bunch.Slug, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);
            var canDelete = !_cashgameService.HasPlayed(player.Id);

            if (canDelete)
            {
                _playerService.Delete(request.PlayerId);
            }

            return new Result(canDelete, bunch.Slug, request.PlayerId);
        }

        public class Request
        {
            public string UserName { get; }
            public string PlayerId { get; }

            public Request(string userName, string playerId)
            {
                UserName = userName;
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