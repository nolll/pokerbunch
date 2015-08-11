using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class TopList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public TopList(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            return Execute(bunch, request.OrderBy, request.Year);
        }

        private Result Execute(Bunch bunch, SortOrder orderBy, int? year)
        {
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, year);
            var players = _playerRepository.GetList(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);

            var items = suite.TotalResults.Select((o, index) => new Item(o, index, bunch.Currency));
            items = SortItems(items, orderBy);

            return new Result(items, orderBy, bunch.Slug, year);
        }

        private static IEnumerable<Item> SortItems(IEnumerable<Item> items, SortOrder orderBy)
        {
            switch (orderBy)
            {
                case SortOrder.WinRate:
                    return items.OrderByDescending(o => o.WinRate);
                case SortOrder.Buyin:
                    return items.OrderByDescending(o => o.Buyin);
                case SortOrder.Cashout:
                    return items.OrderByDescending(o => o.Cashout);
                case SortOrder.TimePlayed:
                    return items.OrderByDescending(o => o.TimePlayed);
                case SortOrder.GamesPlayed:
                    return items.OrderByDescending(o => o.GamesPlayed);
                default:
                    return items.OrderByDescending(o => o.Winnings);
            }
        }

        public class Request
        {
            public string Slug { get; private set; }
            public SortOrder OrderBy { get; private set; }
            public int? Year { get; private set; }

            public Request(string slug, string orderBy, int? year)
                : this(slug, ParseToplistSortOrder(orderBy), year)
            {
            }

            public Request(string slug, SortOrder orderBy, int? year)
            {
                Slug = slug;
                OrderBy = orderBy;
                Year = year;
            }

            private static SortOrder ParseToplistSortOrder(string s)
            {
                if (s == null)
                    return SortOrder.Winnings;
                SortOrder sortOrder;
                return Enum.TryParse(s, true, out sortOrder) ? sortOrder : SortOrder.Winnings;
            }
        }

        public class Result
        {
            public IList<Item> Items { get; private set; }
            public SortOrder OrderBy { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }

            public Result(IEnumerable<Item> items, SortOrder orderBy, string slug, int? year)
            {
                Items = items.ToList();
                OrderBy = orderBy;
                Slug = slug;
                Year = year;
            }
        }

        public class Item
        {
            public int Rank { get; private set; }
            public int PlayerId { get; private set; }
            public string Name { get; private set; }
            public Money Winnings { get; private set; }
            public Money Buyin { get; private set; }
            public Money Cashout { get; private set; }
            public Time TimePlayed { get; private set; }
            public int GamesPlayed { get; private set; }
            public Money WinRate { get; private set; }

            public Item(CashgameTotalResult totalResult, int index, Currency currency)
            {
                Buyin = new Money(totalResult.Buyin, currency);
                Cashout = new Money(totalResult.Cashout, currency);
                GamesPlayed = totalResult.GameCount;
                TimePlayed = Time.FromMinutes(totalResult.TimePlayed);
                Name = totalResult.Player.DisplayName;
                PlayerId = totalResult.Player.Id;
                Rank = index + 1;
                Winnings = new MoneyResult(totalResult.Winnings, currency);
                WinRate = new MoneyWinRate(totalResult.WinRate, currency);
            }
        }

        public enum SortOrder
        {
            Disabled,
            Winnings,
            Buyin,
            Cashout,
            TimePlayed,
            GamesPlayed,
            WinRate,
        }
    }
}