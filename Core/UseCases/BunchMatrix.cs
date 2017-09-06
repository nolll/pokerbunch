using System.Linq;
using Core.Services;

namespace Core.UseCases
{
    public class BunchMatrix : Matrix
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;

        public BunchMatrix(IBunchService bunchService, ICashgameService cashgameService, IPlayerService playerService)
            : base(playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var cashgames = _cashgameService.List(request.Slug, request.Year).Where(o => !o.IsRunning).ToList();
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