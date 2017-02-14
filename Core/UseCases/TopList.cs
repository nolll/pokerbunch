using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class TopList
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public TopList(ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgameCollection = _cashgameRepository.List(request.Slug, request.Year);
            var players = _playerRepository.List(cashgameCollection.Bunch.Id).ToList();
            var suite = new CashgameSuite(cashgameCollection.Cashgames, players);

            var bunch = cashgameCollection.Bunch;
            var items = suite.TotalResults.Select((o, index) => new Item(o, index, cashgameCollection.Bunch.Currency));
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
            public IList<Item> Items { get; private set; }
            public string Slug { get; private set; }
            public string CurrencyFormat { get; private set; }
            public string ThousandSeparator { get; private set; }
            public int? Year { get; private set; }

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
            public int Rank { get; private set; }
            public string PlayerId { get; private set; }
            public string Name { get; private set; }
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