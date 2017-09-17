using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class RunningCashgame
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;
        private readonly IPlayerService _playerService;

        public RunningCashgame(IBunchService bunchService, ICashgameService cashgameService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.BunchId);
            var cashgame = _cashgameService.GetCurrent(request.BunchId);

            if(cashgame == null)
                throw new CashgameNotRunningException();

            var bunchPlayers = _playerService.List(bunch.Id);

            var isManager = RoleHandler.IsInRole(bunch.Role, Role.Manager);
            
            var playerItems = GetPlayerItems(cashgame);
            var bunchPlayerItems = bunchPlayers.Select(o => new BunchPlayerItem(o.Id, o.DisplayName, o.Color)).OrderBy(o => o.Name).ToList();
            
            var defaultBuyin = bunch.DefaultBuyin;
            var currencyFormat = bunch.Currency.Format;
            var thousandSeparator = bunch.Currency.ThousandSeparator;

            return new Result(
                bunch.Id,
                bunch.PlayerId,
                cashgame.Location.Name,
                cashgame.Location.Id,
                playerItems,
                bunchPlayerItems,
                defaultBuyin,
                currencyFormat,
                thousandSeparator,
                isManager);
        }

        private static IList<string> GetPlayerIds(DetailedCashgame cashgame)
        {
            return cashgame.Players.Select(o => o.Id).ToList();
        }

        private static IList<RunningCashgamePlayerItem> GetPlayerItems(DetailedCashgame cashgame)
        {
            var results = GetSortedResults(cashgame);
            var items = new List<RunningCashgamePlayerItem>();
            foreach (var result in results)
            {
                var playerId = result.Id;
                var hasCheckedOut = result.CashoutAction != null;
                var item = new RunningCashgamePlayerItem(playerId, result.Name, result.Color, cashgame.Id, hasCheckedOut, result.Actions);
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
            public string PlayerId { get; }
            public string LocationName { get; }
            public string LocationId { get; }
            public IList<RunningCashgamePlayerItem> PlayerItems { get; }
            public IList<BunchPlayerItem> BunchPlayerItems { get; }
            public int DefaultBuyin { get; }
            public string CurrencyFormat { get; }
            public string ThousandSeparator { get; }
            public bool IsManager { get; }

            public Result(
                string slug,
                string playerId,
                string locationName,
                string locationId,
                IList<RunningCashgamePlayerItem> playerItems,
                IList<BunchPlayerItem> bunchPlayerItems,
                int defaultBuyin,
                string currencyFormat,
                string thousandSeparator,
                bool isManager)
            {
                Slug = slug;
                PlayerId = playerId;
                LocationName = locationName;
                LocationId = locationId;
                PlayerItems = playerItems;
                BunchPlayerItems = bunchPlayerItems;
                DefaultBuyin = defaultBuyin;
                CurrencyFormat = currencyFormat;
                ThousandSeparator = thousandSeparator;
                IsManager = isManager;
            }
        }

        public class BunchPlayerItem
        {
            public string PlayerId { get; }
            public string Name { get; }
            public string Color { get; }

            public BunchPlayerItem(string playerId, string name, string color)
            {
                PlayerId = playerId;
                Name = name;
                Color = color;
            }
        }

        public class RunningCashgameCheckpointItem
        {
            public DateTime Time { get; }
            public int Stack { get; }
            public int AddedMoney { get; }

            public RunningCashgameCheckpointItem(DetailedCashgame.CashgameAction action)
            {
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
            public bool HasCashedOut { get; }
            public int Buyin { get; }
            public int Stack { get; }
            public int Winnings { get; }
            public DateTime LastReport { get; }
            public IList<RunningCashgameCheckpointItem> Checkpoints { get; }

            public RunningCashgamePlayerItem(string playerId, string name, string color, string cashgameId, bool hasCashedOut, IList<DetailedCashgame.CashgameAction> actions)
            {
                PlayerId = playerId;
                Name = name;
                Color = color;
                CashgameId = cashgameId;
                HasCashedOut = hasCashedOut;
                var list = actions.ToList();
                var lastCheckpoint = list.Last();
                Checkpoints = list.Select(o => new RunningCashgameCheckpointItem(o)).ToList();
                Buyin = list.Sum(o => o.Added);
                Stack = lastCheckpoint.Stack;
                Winnings = Stack - Buyin;
                LastReport = lastCheckpoint.Time;
            }
        }
    }
}
