using Core.Repositories;

namespace Core.UseCases
{
    public class BunchMatrix : Matrix
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public BunchMatrix(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
            : base(playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var cashgames = _cashgameRepository.List(request.Slug, request.Year);
            return Execute(bunch, cashgames);
        }

        public class Request
        {
            public string Slug { get; }
            public int? Year { get; }

            public Request(string slug, int? year)
            {
                Slug = slug;
                Year = year;
            }
        }
    }
}