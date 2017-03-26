using Core.Services;

namespace Core.UseCases
{
    public class EndCashgame
    {
        private readonly ICashgameService _cashgameService;

        public EndCashgame(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameService.GetCurrent(request.Slug);
            _cashgameService.End(cashgame.Id);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }
    }
}
