using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameStatus
    {
        private readonly BunchService _bunchService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public CashgameStatus(BunchService bunchService, ICashgameRepository cashgameRepository, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _cashgameRepository = cashgameRepository;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var runningGame = _cashgameRepository.GetRunning(bunch.Id);

            var gameIsRunning = runningGame != null;

            return new Result(
                request.Slug,
                gameIsRunning);
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
            public string Slug { get; private set; }
            public bool GameIsRunning { get; private set; }

            public Result(
                string slug,
                bool gameIsRunning)
            {
                Slug = slug;
                GameIsRunning = gameIsRunning;
            }
        }
    }
}