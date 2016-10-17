using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class Matrix
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly UserService _userService;
        private readonly EventService _eventService;

        public Matrix(IBunchRepository bunchRepository, CashgameService cashgameService, PlayerService playerService, UserService userService, EventService eventServicey)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userService = userService;
            _eventService = eventServicey;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var cashgames = _cashgameService.GetFinished(bunch.Id, request.Year);
            return Execute(bunch, cashgames);
        }

        public Result Execute(EventMatrixRequest request)
        {
            var e = _eventService.Get(request.EventId);
            var bunch = _bunchRepository.Get(e.Bunch);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var cashgames = _cashgameService.GetByEvent(request.EventId);
            return Execute(bunch, cashgames);
        }

        private Result Execute(Bunch bunch, IList<Cashgame> cashgames)
        {
            var players = _playerService.GetList(bunch.Id);
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

        private static IDictionary<string, MatrixResultItem> CreatePlayerResultItems(Bunch bunch, IEnumerable<Cashgame> cashgames, Player player)
        {
            var items = new Dictionary<string, MatrixResultItem>();
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
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

        private static List<GameItem> CreateGameItems(IEnumerable<Cashgame> cashgames)
        {
            return cashgames
                .Where(o => o.StartTime.HasValue)
                .OrderByDescending(o => o.StartTime)
                .Select(o => CreateGameItem(o.Id, o.StartTime.Value))
                .ToList();
        }

        private static GameItem CreateGameItem(string cashgameId, DateTime startTime)
        {
            var date = new Date(startTime);
            
            return new GameItem(cashgameId, date);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            public int? Year { get; }

            public Request(string userName, string slug, int? year)
            {
                UserName = userName;
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