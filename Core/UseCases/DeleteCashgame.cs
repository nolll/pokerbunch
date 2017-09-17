using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly ICashgameService _cashgameService;

        public DeleteCashgame(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.Id);

            if (cashgame.Players.Count > 0)
                throw new CashgameHasResultsException();

            _cashgameService.DeleteGame(cashgame.Id);

            return new Result(cashgame.Bunch.Id);
        }

        public class Request
        {
            public string Id { get; }

            public Request(string id)
            {
                Id = id;
            }
        }

        public class Result
        {
            public string Slug { get; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}
