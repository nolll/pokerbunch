using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class CurrentRankings
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;
        private readonly IPlayerService _playerService;

        public CurrentRankings(IBunchService bunchService, ICashgameService cashgameService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var years = _cashgameService.GetYears(request.Slug);
            var latestYear = years.Count > 0 ? years.OrderBy(o => o).Last() : (int?)null;
            var cashgames = _cashgameService.List(request.Slug, latestYear).Where(o => !o.IsRunning).ToList();
            if (!cashgames.Any())
                return new Result(new List<Item>(), "");

            var players = _playerService.List(request.Slug).ToList();
            var suite = new CashgameSuite(cashgames, players);
            var lastGame = cashgames.First();
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
            public string Slug { get; }

            public Request(string slug)
            {
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