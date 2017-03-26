using System;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.UseCases
{
    public class EditCheckpointForm
    {
        private readonly ICashgameService _cashgameService;

        public EditCheckpointForm(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            var action = cashgame.GetAction(request.CheckpointId);
            var stack = action.Stack;
            var amount = action.Added;
            var timestamp = TimeZoneInfo.ConvertTime(action.Time, cashgame.Bunch.Timezone);
            var canEditAmount = action.Type == CheckpointType.Buyin;

            return new Result(stack, amount, timestamp, action.Id, cashgame.Id, action.PlayerId, cashgame.Bunch.Id, canEditAmount);
        }

        public class Request
        {
            public string CashgameId { get; }
            public string CheckpointId { get; }

            public Request(string cashgameId, string checkpointId)
            {
                CashgameId = cashgameId;
                CheckpointId = checkpointId;
            }
        }

        public class Result
        {
            public int Stack { get; private set; }
            public int Amount { get; private set; }
            public DateTime TimeStamp { get; private set; }
            public string ActionId { get; private set; }
            public string CashgameId { get; private set; }
            public string PlayerId { get; private set; }
            public string Slug { get; private set; }
            public bool CanEditAmount { get; private set; }

            public Result(int stack, int amount, DateTime timeStamp, string actionId, string cashgameId, string playerId, string slug, bool canEditAmount)
            {
                TimeStamp = timeStamp;
                ActionId = actionId;
                Stack = stack;
                Amount = amount;
                CashgameId = cashgameId;
                PlayerId = playerId;
                Slug = slug;
                CanEditAmount = canEditAmount;
            }
        }
    }
}