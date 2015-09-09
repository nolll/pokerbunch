using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class InvitePlayerForm
    {
        private readonly BunchService _bunchService;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public InvitePlayerForm(BunchService bunchService, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchService = bunchService;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.GetById(request.PlayerId);
            var bunch = _bunchService.Get(player.BunchId);
            var currentUser = _userService.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(bunch.Id, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);

            return new Result(bunch.Slug);
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
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}