using Core.Repositories;

namespace Core.UseCases
{
    public class EndCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;

        public EndCashgame(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public void Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetCurrent(request.Slug);
            _cashgameRepository.End(cashgame.Id);
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
