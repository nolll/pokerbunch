using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class Matrix
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public Matrix(ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgames = _cashgameRepository.List(request.Slug, request.Year);
            return Execute(cashgames);
        }

        public Result Execute(EventMatrixRequest request)
        {
            var cashgames = _cashgameRepository.EventList(request.EventId);
            return Execute(cashgames);
        }

        private Result Execute(CashgameCollection cashgameCollection)
        {
            var players = _playerRepository.List(cashgameCollection.Bunch.Id);
            var suite = new CashgameSuite(cashgameCollection.Cashgames, players);

            var gameItems = CreateGameItems(cashgameCollection.Cashgames);
            var playerItems = CreatePlayerItems(cashgameCollection.Bunch, suite);
            var spansMultipleYears = suite.SpansMultipleYears;

            return new Result(gameItems, playerItems, spansMultipleYears);
        }

        private static IList<MatrixPlayerItem> CreatePlayerItems(CashgameBunch bunch, CashgameSuite suite)
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

        private static IDictionary<string, MatrixResultItem> CreatePlayerResultItems(CashgameBunch bunch, IEnumerable<ListCashgame> cashgames, Player player)
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

        public class Request
        {
            public string Slug { get; }
            public int? Year { get; }

            public Request(string slug, int? year)
            {
                Slug = slug;
                Year = year;
            }
        }

        public class EventMatrixRequest
        {
            public string UserName { get; }
            public string EventId { get; }

            public EventMatrixRequest(string userName, string eventId)
            {
                UserName = userName;
                EventId = eventId;
            }
        }

        public class Result
        {
            public IList<GameItem> GameItems { get; private set; }
            public IList<MatrixPlayerItem> PlayerItems { get; private set; }
            public bool SpansMultipleYears { get; private set; }

            public Result(IList<GameItem> gameItems, IList<MatrixPlayerItem> playerItems, bool spansMultipleYears)
            {
                GameItems = gameItems;
                PlayerItems = playerItems;
                SpansMultipleYears = spansMultipleYears;
            }
        }

        public class MatrixPlayerItem
        {
            public int Rank { get; private set; }
            public string Name { get; private set; }
            public string Color { get; private set; }
            public string PlayerId { get; private set; }
            public IDictionary<string, MatrixResultItem> ResultItems { get; private set; }
            public Money TotalResult { get; private set; }

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
            public Money Buyin { get; private set; }
            public Money Cashout { get; private set; }
            public Money Winnings { get; private set; }
            public bool HasBestResult { get; private set; }
            public bool HasTransactions { get; private set; }

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
            public string Id { get; private set; }
            public Date Date { get; private set; }

            public GameItem(string id, Date date)
            {
                Id = id;
                Date = date;
            }
        }
    }
}