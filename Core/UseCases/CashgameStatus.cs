using Core.Services;

namespace Core.UseCases
{
    public class CashgameStatus
    {
        private readonly ICashgameService _cashgameService;

        public CashgameStatus(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var runningGame = _cashgameService.GetCurrent(request.Slug);

            var gameIsRunning = runningGame != null;

            return new Result(
                request.Slug,
                gameIsRunning);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public string Slug { get; }
            public bool GameIsRunning { get; }

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