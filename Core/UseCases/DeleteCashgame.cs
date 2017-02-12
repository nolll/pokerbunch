using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCashgame(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetDetailedById(request.Id);

            if (cashgame.Players.Count > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame.Id);

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
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}
