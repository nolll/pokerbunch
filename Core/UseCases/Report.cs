using Core.Services;

namespace Core.UseCases
{
    public class Report
    {
        private readonly ICashgameService _cashgameService;

        public Report(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameService.GetCurrent(request.Slug);

            _cashgameService.Report(cashgame.Id, request.PlayerId, request.Stack);
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
    }
}
