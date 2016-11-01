using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class InvitePlayerForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly PlayerService _playerService;
        private readonly IUserRepository _userRepository;

        public InvitePlayerForm(IBunchRepository bunchRepository, PlayerService playerService, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerService = playerService;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUser(bunch.Id, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);

            return new Result(bunch.Id);
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
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}