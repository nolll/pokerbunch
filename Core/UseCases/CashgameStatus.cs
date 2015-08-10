using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class CashgameStatus
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameStatus(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(bunch.Id);

            var gameIsRunning = runningGame != null;

            return new Result(
                request.Slug,
                gameIsRunning);
        }

        public class Request
        {
            public string Slug { get; private set; }

            public Request(string slug)
            {
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