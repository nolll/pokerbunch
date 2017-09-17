using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public abstract class Matrix
    {
        private readonly IPlayerService _playerService;

        protected Matrix(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        protected Result Execute(Bunch bunch, IList<ListCashgame> cashgames)
        {
            var players = _playerService.List(bunch.Id);
            var suite = new CashgameSuite(cashgames, players);

            var gameItems = CreateGameItems(cashgames);
            var playerItems = CreatePlayerItems(bunch, suite);
            var spansMultipleYears = suite.SpansMultipleYears;

            return new Result(gameItems, playerItems, spansMultipleYears);
        }

        private static IList<MatrixPlayerItem> CreatePlayerItems(Bunch bunch, CashgameSuite suite)
        {
            var index = 0;
            var playerItems = new List<MatrixPlayerItem>();
            foreach (var totalResult in suite.TotalResults)
            {
                var p = totalResult.Player;
                var rank = ++index;
                var name = p.DisplayName;
                var color = p.Color;
                var results = CreatePlayerResultItems(bunch, suite.Cashgames, p);
                var winnings = new Money(totalResult.Winnings, bunch.Currency);
                var playerItem = new MatrixPlayerItem(rank, name, color, p.Id, results, winnings);
                playerItems.Add(playerItem);
            }
            return playerItems;
        }

        private static IDictionary<string, MatrixResultItem> CreatePlayerResultItems(Bunch bunch, IEnumerable<ListCashgame> cashgames, Player player)
        {
            var items = new Dictionary<string, MatrixResultItem>();
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.Players.FirstOrDefault(o => o.Id == player.Id);
                if (result != null)
                {
                    var hasTransactions = result.Buyin > 0;
                    var buyin = new Money(result.Buyin, bunch.Currency);
                    var cashout = new Money(result.Stack, bunch.Currency);
                    var winnings = new Money(result.Winnings, bunch.Currency);
                    var hasBestResult = cashgame.IsBestResult(result);
                    var item = new MatrixResultItem(buyin, cashout, winnings, hasBestResult, hasTransactions);
                    items.Add(cashgame.Id, item);
                }
            }
            return items;
        }

        private static List<GameItem> CreateGameItems(IEnumerable<ListCashgame> cashgames)
        {
            return cashgames
                .OrderByDescending(o => o.StartTime)
                .Select(o => CreateGameItem(o.Id, o.StartTime))
                .ToList();
        }

        private static GameItem CreateGameItem(string cashgameId, DateTime startTime)
        {
            var date = new Date(startTime);
            
            return new GameItem(cashgameId, date);
        }

        public class Result
        {
            public IList<GameItem> GameItems { get; }
            public IList<MatrixPlayerItem> PlayerItems { get; }
            public bool SpansMultipleYears { get; }

            public Result(IList<GameItem> gameItems, IList<MatrixPlayerItem> playerItems, bool spansMultipleYears)
            {
                GameItems = gameItems;
                PlayerItems = playerItems;
                SpansMultipleYears = spansMultipleYears;
            }
        }

        public class MatrixPlayerItem
        {
            public int Rank { get; }
            public string Name { get; }
            public string Color { get; }
            public string PlayerId { get; }
            public IDictionary<string, MatrixResultItem> ResultItems { get; }
            public Money TotalResult { get; }

            public MatrixPlayerItem(int rank, string name, string color, string playerId, IDictionary<string, MatrixResultItem> resultItems, Money totalResult)
            {
                Rank = rank;
                Name = name;
                Color = color;
                PlayerId = playerId;
                ResultItems = resultItems;
                TotalResult = totalResult;
            }
        }

        public class MatrixResultItem
        {
            public Money Buyin { get; }
            public Money Cashout { get; }
            public Money Winnings { get; }
            public bool HasBestResult { get; }
            public bool HasTransactions { get; }

            public MatrixResultItem(Money buyin, Money cashout, Money winnings, bool hasBestResult, bool hasTransactions)
            {
                Buyin = buyin;
                Cashout = cashout;
                Winnings = winnings;
                HasBestResult = hasBestResult;
                HasTransactions = hasTransactions;
            }
        }

        public class GameItem
        {
            public string Id { get; }
            public Date Date { get; }

            public GameItem(string id, Date date)
            {
                Id = id;
                Date = date;
            }
        }
    }
}