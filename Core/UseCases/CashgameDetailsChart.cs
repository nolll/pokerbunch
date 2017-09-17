using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameDetailsChart
    {
        private readonly ICashgameService _cashgameService;

        public CashgameDetailsChart(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            var playerItems = GetPlayerItems(cashgame);
            return new Result(playerItems);
        }

        private static IList<PlayerItem> GetPlayerItems(DetailedCashgame cashgame)
        {
            var playerItems = new List<PlayerItem>();
            foreach (var player in cashgame.Players)
            {
                var resultItems = new List<ResultItem>();
                var totalBuyin = 0;
                foreach (var actions in player.Actions)
                {
                    if (actions.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += actions.Added;
                    }
                    var localTime = TimeZoneInfo.ConvertTime(actions.Time, cashgame.Bunch.Timezone);
                    var winnings = actions.Stack - totalBuyin;
                    resultItems.Add(new ResultItem(localTime, winnings));
                }
                if (cashgame.IsRunning)
                {
                    var timestamp = TimeZoneInfo.ConvertTime(cashgame.UpdatedTime, cashgame.Bunch.Timezone);
                    resultItems.Add(new ResultItem(timestamp, player.Winnings));
                }
                playerItems.Add(new PlayerItem(player.Id, player.Name, player.Color, resultItems));
            }
            return playerItems;
        }

        public class Request
        {
            public string CashgameId { get; }

            public Request(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }

        public class Result
        {
            public IList<PlayerItem> PlayerItems { get; }

            public Result(IList<PlayerItem> playerItems)
            {
                PlayerItems = playerItems;
            }
        }

        public class PlayerItem
        {
            public string Id { get; }
            public string Name { get; }
            public string Color { get; }
            public IList<ResultItem> Results { get; }

            public PlayerItem(string id, string name, string color, IList<ResultItem> results)
            {
                Id = id;
                Name = name;
                Color = color;
                Results = results;
            }
        }

        public class ResultItem
        {
            public DateTime Timestamp { get; }
            public int Winnings { get; }

            public ResultItem(DateTime timestamp, int winnings)
            {
                Timestamp = timestamp;
                Winnings = winnings;
            }
        }
    }
}