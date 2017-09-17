using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class TopList
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;
        private readonly IPlayerService _playerService;

        public TopList(IBunchService bunchService, ICashgameService cashgameService, IPlayerService playerService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var cashgames = _cashgameService.List(request.Slug, request.Year).Where(o => !o.IsRunning).ToList();
            var players = _playerService.List(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);

            var items = suite.TotalResults.Select((o, index) => new Item(o, index, bunch.Currency));
            items = SortItems(items);

            return new Result(items, bunch.Id, bunch.Currency.Format, bunch.Currency.ThousandSeparator, request.Year);
        }

        private static IEnumerable<Item> SortItems(IEnumerable<Item> items)
        {
            return items.OrderByDescending(o => o.Winnings);
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

        public class Result
        {
            public IList<Item> Items { get; }
            public string Slug { get; }
            public string CurrencyFormat { get; }
            public string ThousandSeparator { get; }
            public int? Year { get; }

            public Result(IEnumerable<Item> items, string slug, string currencyFormat, string thousandSeparator, int? year)
            {
                Items = items.ToList();
                Slug = slug;
                CurrencyFormat = currencyFormat;
                ThousandSeparator = thousandSeparator;
                Year = year;
            }
        }

        public class Item
        {
            public int Rank { get; }
            public string PlayerId { get; }
            public string Name { get; }
            public Money Winnings { get; }
            public Money Buyin { get; }
            public Money Cashout { get; }
            public Time TimePlayed { get; }
            public int GamesPlayed { get; }
            public Money WinRate { get; }

            public Item(CashgameTotalResult totalResult, int index, Currency currency)
            {
                Buyin = new Money(totalResult.Buyin, currency);
                Cashout = new Money(totalResult.Cashout, currency);
                GamesPlayed = totalResult.GameCount;
                TimePlayed = Time.FromMinutes(totalResult.TimePlayed);
                Name = totalResult.Player.DisplayName;
                PlayerId = totalResult.Player.Id;
                Rank = index + 1;
                Winnings = new Money(totalResult.Winnings, currency);
                WinRate = new Money(totalResult.WinRate, currency);
            }
        }
    }
}