using Core.Services;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly ICashgameService _cashgameService;

        public EditCashgame(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.Update(request.Id, request.LocationId, request.EventId);
            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string Id { get; }
            public string LocationId { get; }
            public string EventId { get; }

            public Request(string id, string locationId, string eventId)
            {
                Id = id;
                LocationId = locationId;
                EventId = eventId;
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
