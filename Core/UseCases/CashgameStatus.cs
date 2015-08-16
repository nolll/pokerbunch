using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class CashgameStatus
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public CashgameStatus(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
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
            public bool GameIsRunning { get; private set; }
            public Url RunningCashgameUrl { get; private set; }
            public Url AddCashgameUrl { get; private set; }

            public Result(
                string slug,
                bool gameIsRunning)
            {
                GameIsRunning = gameIsRunning;
                RunningCashgameUrl = new RunningCashgameUrl(slug);
                AddCashgameUrl = new AddCashgameUrl(slug);
            }
        }
    }
}