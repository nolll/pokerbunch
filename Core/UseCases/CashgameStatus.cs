using Core.Repositories;

namespace Core.UseCases
{
    public class CashgameStatus
    {
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameStatus(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var runningGame = _cashgameRepository.GetRunning(request.Slug);

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