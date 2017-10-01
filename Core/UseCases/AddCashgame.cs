using Core.Services;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly ICashgameService _cashgameService;

        public AddCashgame(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgameId = _cashgameService.Add(request.Slug, request.LocationId);

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string Slug { get; }
            public string LocationId { get; }

            public Request(string slug, string locationId)
            {
                Slug = slug;
                LocationId = locationId;
            }
        }

        public class Result
        {
            public string Slug { get; }
            public string CashgameId { get; }

            public Result(string slug, string cashgameId)
            {
                Slug = slug;
                CashgameId = cashgameId;
            }
        }
    }
}