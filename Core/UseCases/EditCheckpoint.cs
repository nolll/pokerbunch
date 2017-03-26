using System;
using Core.Services;

namespace Core.UseCases
{
    public class EditCheckpoint
    {
        private readonly ICashgameService _cashgameService;

        public EditCheckpoint(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            var existingCheckpoint = cashgame.GetAction(request.ActionId);
            
            _cashgameService.UpdateAction(existingCheckpoint.Id, request.Timestamp, request.Stack, request.Amount);

            return new Result(cashgame.Id, existingCheckpoint.PlayerId);
        }

        public class Request
        {
            public string CashgameId { get; }
            public string ActionId { get; }
            public DateTime Timestamp { get; }
            public int Stack { get; }
            public int Amount { get; }

            public Request(string cashgameId, string actionId, DateTime timestamp, int stack, int amount)
            {
                CashgameId = cashgameId;
                ActionId = actionId;
                Timestamp = timestamp;
                Stack = stack;
                Amount = amount;
            }
        }

        public class Result
        {
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }

            public Result(string cashgameId, string playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }
    }
}
