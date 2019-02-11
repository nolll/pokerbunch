using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class RunningCashgame
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;

        public RunningCashgame(IBunchService bunchService, ICashgameService cashgameService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.BunchId);
            var cashgame = _cashgameService.GetCurrent(request.BunchId);

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var playerItems = GetPlayerItems(cashgame);

            return new Result(
                bunch.Id,
                cashgame.Location.Name,
                cashgame.Location.Id,
                playerItems);
        }

        private static IList<RunningCashgamePlayerItem> GetPlayerItems(DetailedCashgame cashgame)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgamePlayerItem>();
            foreach (var result in results)
            {
                var playerId = result.Id;
                var item = new RunningCashgamePlayerItem(playerId, result.Name, result.Color, cashgame.Id, result.Actions);
                items.Add(item);
            }

            return items;
        }

        private static IEnumerable<DetailedCashgame.CashgamePlayer> GetSortedResults(DetailedCashgame cashgame)
        {
            var results = cashgame.Players;
            return results.OrderByDescending(o => o.Winnings);
        }

        public class Request
        {
            public string UserName { get; }
            public string BunchId { get; }

            public Request(string userName, string bunchId)
            {
                UserName = userName;
                BunchId = bunchId;
            }
        }

        public class Result
        {
            public string Slug { get; }
            public string LocationName { get; }
            public string LocationId { get; }
            public IList<RunningCashgamePlayerItem> PlayerItems { get; }

            public Result(
                string slug,
                string locationName,
                string locationId,
                IList<RunningCashgamePlayerItem> playerItems)
            {
                Slug = slug;
                LocationName = locationName;
                LocationId = locationId;
                PlayerItems = playerItems;
            }
        }

        public class RunningCashgameCheckpointItem
        {
            public CheckpointType Type { get; }
            public DateTime Time { get; }
            public int Stack { get; }
            public int AddedMoney { get; }

            public RunningCashgameCheckpointItem(DetailedCashgame.CashgameAction action)
            {
                Type = action.Type;
                Time = action.Time;
                Stack = action.Stack;
                AddedMoney = action.Added;
            }
        }

        public class RunningCashgamePlayerItem
        {
            public string PlayerId { get; }
            public string Name { get; }
            public string Color { get; }
            public string CashgameId { get; }
            public IList<RunningCashgameCheckpointItem> Checkpoints { get; }

            public RunningCashgamePlayerItem(string playerId, string name, string color, string cashgameId, IList<DetailedCashgame.CashgameAction> actions)
            {
                PlayerId = playerId;
                Name = name;
                Color = color;
                CashgameId = cashgameId;
                var list = actions.ToList();
                Checkpoints = list.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
            }
        }
    }
}
