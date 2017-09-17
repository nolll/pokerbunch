using Core.Services;

namespace Core.UseCases
{
    public class Cashout
    {
        private readonly ICashgameService _cashgameService;

        public Cashout(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetCurrent(request.Slug);

            _cashgameService.Cashout(cashgame.Id, request.PlayerId, request.Stack);

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
            public string CashgameId { get; }

            public Result(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
