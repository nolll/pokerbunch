using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class DeletePlayer
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly UserService _userService;
        private readonly BunchService _bunchService;

        public DeletePlayer(IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, UserService userService, BunchService bunchService)
        {
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _userService = userService;
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchService.Get(player.BunchId);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);
            var canDelete = !_cashgameRepository.HasPlayed(player.Id);

            if (canDelete)
            {
                _playerRepository.Delete(request.PlayerId);
            }

            return new Result(canDelete, bunch.Slug, request.PlayerId);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int PlayerId { get; private set; }

            public Request(string userName, int playerId)
            {
                UserName = userName;
                PlayerId = playerId;
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