using Core.Repositories;

namespace Core.UseCases
{
    public class Cashout
    {
        private readonly ICashgameRepository _cashgameRepository;

        public Cashout(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetCurrent(request.Slug);

            _cashgameRepository.Cashout(cashgame.Id, request.PlayerId, request.Stack);

            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string Slug { get; }
            public string PlayerId { get; }
            public int Stack { get; }

            public Request(string slug, string playerId, int stack)
            {
                Slug = slug;
                PlayerId = playerId;
                Stack = stack;
            }
        }

        public class Result
        {
            public string CashgameId { get; private set; }

            public Result(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
