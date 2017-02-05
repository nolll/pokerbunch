using Core.Repositories;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgame(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.Update(request.Id, request.LocationId, request.EventId);
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
            public string CashgameId { get; private set; }

            public Result(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
