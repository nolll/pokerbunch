using Core.Repositories;

namespace Core.UseCases
{
    public class Report
    {
        private readonly ICashgameRepository _cashgameRepository;

        public Report(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetCurrent(request.Slug);

            _cashgameRepository.Report(cashgame.Id, request.PlayerId, request.Stack);
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
