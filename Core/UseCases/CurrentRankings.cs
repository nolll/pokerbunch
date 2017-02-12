using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CurrentRankings
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public CurrentRankings(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUser(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var years = _cashgameRepository.GetYears(bunch.Id);
            var latestYear = years.Count > 0 ? years.OrderBy(o => o).Last() : (int?)null;
            var cashgames = _cashgameRepository.List(bunch.Id, latestYear);
            if (!cashgames.Any())
                return new Result(new List<Item>(), "");

            var players = _playerRepository.List(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);
            var lastGame = cashgames.Last();
            var items = CreateItems(bunch, suite, lastGame);
            
            return new Result(items, lastGame.Id);
        }
        
        private IEnumerable<Item> CreateItems(Bunch bunch, CashgameSuite suite, ListCashgame lastGame)
        {
            var items = new List<Item>();
            var index = 1;
            foreach (var totalResult in suite.TotalResults)
            {
                var lastGameResult = lastGame.Players.FirstOrDefault(o => o.Id == totalResult.Player.Id);
                var item = new Item(totalResult, lastGameResult, index++, bunch.Currency);
                items.Add(item);
            }
            return items;
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Items { get; }
            public string LastGameId { get; }
            public bool HasGames => Items.Any();

            public Result(IEnumerable<Item> items, string lastGameId)
            {
                Items = items.ToList();
                LastGameId = lastGameId;
            }
        }

        public class Item
        {
            public int Rank { get; }
            public string PlayerId { get; }
            public string Name { get; }
            public Money TotalWinnings { get; }
            public Money LastGameWinnings { get; }

            public Item(CashgameTotalResult totalResult, ListCashgame.CashgamePlayer lastGameResult, int index, Currency currency)
            {
                Rank = index;
                PlayerId = totalResult.Player.Id;
                Name = totalResult.Player.DisplayName;
                TotalWinnings = new Money(totalResult.Winnings, currency);
                LastGameWinnings = lastGameResult != null ? new Money(lastGameResult.Winnings, currency) : null;
            }
        }
    }
}