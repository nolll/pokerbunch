using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class JoinBunchConfirmation
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public JoinBunchConfirmation(BunchService bunchService, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var bunchName = bunch.DisplayName;

            return new Result(bunchName, bunch.Slug);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

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