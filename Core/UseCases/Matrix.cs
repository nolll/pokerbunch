using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class Matrix
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public Matrix(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IUserRepository userRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            return Execute(bunch, cashgames);
        }

        public Result Execute(EventMatrixRequest request)
        {
            var e = _eventRepository.GetById(request.EventId);
            var bunch = _bunchRepository.GetById(e.Id);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgames = _cashgameRepository.GetByEvent(request.EventId);
            return Execute(bunch, cashgames);
        }

        private Result Execute(Bunch bunch, IList<Cashgame> cashgames)
        {
            var players = _playerRepository.GetList(bunch.Id);
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
                var results = CreatePlayerResultItems(bunch, suite.Cashgames, p);
                var winnings = new Money(totalResult.Winnings, bunch.Currency);
                var playerItem = new MatrixPlayerItem(rank, name, p.Id, results, winnings);
                playerItems.Add(playerItem);
            }
            return playerItems;
        }

        private static IDictionary<int, MatrixResultItem> CreatePlayerResultItems(Bunch bunch, IEnumerable<Cashgame> cashgames, Player player)
        {
            var items = new Dictionary<int, MatrixResultItem>();
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

        private static GameItem CreateGameItem(int cashgameId, DateTime startTime)
        {
            var date = new Date(startTime);
            var url = new CashgameDetailsUrl(cashgameId);
            
            return new GameItem(cashgameId, date, url);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }

            public Request(string userName, string slug, int? year)
            {
                UserName = userName;
                Slug = slug;
                Year = year;
            }
        }

        public class EventMatrixRequest
        {
            public string UserName { get; private set; }
            public int EventId { get; private set; }

            public EventMatrixRequest(string userName, int eventId)
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
            public int PlayerId { get; private set; }
            public IDictionary<int, MatrixResultItem> ResultItems { get; private set; }
            public Money TotalResult { get; private set; }

            public MatrixPlayerItem(int rank, string name, int playerId, IDictionary<int, MatrixResultItem> resultItems, Money totalResult)
            {
                Rank = rank;
                Name = name;
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
            public int Id { get; private set; }
            public Date Date { get; private set; }
            public Url Url { get; private set; }

            public GameItem(int id, Date date, Url url)
            {
                Id = id;
                Date = date;
                Url = url;
            }
        }
    }
}