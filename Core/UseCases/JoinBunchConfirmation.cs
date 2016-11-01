using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class JoinBunchConfirmation
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly PlayerService _playerService;

        public JoinBunchConfirmation(IBunchRepository bunchRepository, IUserRepository userRepository, PlayerService playerService)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUser(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var bunchName = bunch.DisplayName;

            return new Result(bunchName, bunch.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public string BunchName { get; private set; }
            public string Slug { get; private set; }

            public Result(string bunchName, string slug)
            {
                Slug = slug;
                BunchName = bunchName;
            }
        }
    }
}