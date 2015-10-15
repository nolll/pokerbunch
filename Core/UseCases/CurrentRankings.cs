using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class CurrentRankings
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly PlayerService _playerService;
        private readonly UserService _userService;

        public CurrentRankings(BunchService bunchService, CashgameService cashgameService, PlayerService playerService, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var years = _cashgameService.GetYears(bunch.Id);
            var latestYear = years.Count > 0 ? years.OrderBy(o => o).Last() : (int?)null;
            var cashgames = _cashgameService.GetFinished(bunch.Id, latestYear);
            if (!cashgames.Any())
                return new Result(new List<Item>(), 0);

            var players = _playerService.GetList(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);
            var lastGame = cashgames.Last();
            var items = CreateItems(bunch, suite, lastGame);
            
            return new Result(items, lastGame.Id);
        }
        
        private IEnumerable<Item> CreateItems(Bunch bunch, CashgameSuite suite, Cashgame lastGame)
        {
            var items = new List<Item>();
            var index = 1;
            foreach (var totalResult in suite.TotalResults)
            {
                var lastGameResult = lastGame.GetResult(totalResult.Player.Id);
                var item = new Item(totalResult, lastGameResult, index++, bunch.Currency);
                items.Add(item);
            }
            return items;
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Items { get; private set; }
            public int LastGameId { get; private set; }

            public Result(IEnumerable<Item> items, int lastGameId)
            {
                Items = items.ToList();
                LastGameId = lastGameId;
            }

            public bool HasGames
            {
                get { return Items.Any(); }
            }
        }

        public class Item
        {
            public int Rank { get; private set; }
            public int PlayerId { get; private set; }
            public string Name { get; private set; }
            public Money TotalWinnings { get; private set; }
            public Money LastGameWinnings { get; private set; }

            public Item(CashgameTotalResult totalResult, CashgameResult lastGameResult, int index, Currency currency)
            {
                Rank = index;
                PlayerId = totalResult.Player.Id;
                Name = totalResult.Player.DisplayName;
                TotalWinnings = new MoneyResult(totalResult.Winnings, currency);
                LastGameWinnings = lastGameResult != null ? new MoneyResult(lastGameResult.Winnings, currency) : null;
            }
        }
    }
}