using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.UseCases
{
    public class ActionsChart
    {
        private readonly ICashgameService _cashgameService;

        public ActionsChart(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            
            var result = cashgame.Players.First(o => o.Id == request.PlayerId);

            var checkpointItems = GetCheckpointItems(cashgame, result);

            return new Result(checkpointItems);
        }

        private static IList<CheckpointItem> GetCheckpointItems(DetailedCashgame cashgame, DetailedCashgame.CashgamePlayer player)
        {
            var checkpointItems = new List<CheckpointItem>();
            var totalBuyin = 0;
            foreach (var action in player.Actions)
            {
                var stack = action.Stack;
                var addedMoney = 0;
                if (action.Type == CheckpointType.Buyin)
                {
                    if (totalBuyin > 0)
                    {
                        addedMoney = action.Added;
                    }
                    totalBuyin += action.Added;
                }
                var localTime = TimeZoneInfo.ConvertTime(action.Time, cashgame.Bunch.Timezone);
                checkpointItems.Add(new CheckpointItem(localTime, stack, totalBuyin, addedMoney));
            }
            if (cashgame.IsRunning)
            {
                var localTime = TimeZoneInfo.ConvertTime(cashgame.UpdatedTime, cashgame.Bunch.Timezone);
                checkpointItems.Add(new CheckpointItem(localTime, player.Stack, player.Buyin));
            }
            return checkpointItems;
        }

        public class Request
        {
            public string CashgameId { get; }
            public string PlayerId { get; }

            public Request(string cashgameId, string playerId)
            {
                CashgameId = cashgameId;
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public IList<CheckpointItem> CheckpointItems { get; private set; }

            public Result(IList<CheckpointItem> checkpointItems)
            {
                CheckpointItems = checkpointItems;
            }
        }

        public class CheckpointItem
        {
            public DateTime Timestamp { get; private set; }
            public int Stack { get; private set; }
            public int TotalBuyin { get; private set; }
            public int AddedMoney { get; private set; }

            public CheckpointItem(DateTime timestamp, int stack, int totalBuyin, int addedMoney = 0)
            {
                Timestamp = timestamp;
                Stack = stack;
                TotalBuyin = totalBuyin;
                AddedMoney = addedMoney;
            }
        }
    }
}