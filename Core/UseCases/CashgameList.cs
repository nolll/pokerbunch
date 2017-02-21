using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class CashgameList
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameList(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var cashgames = _cashgameRepository.List(bunch.Id, request.Year).Where(o => !o.IsRunning);
            cashgames = SortItems(cashgames, request.SortOrder);
            var list = cashgames.Select(o => new Item(bunch, o));

            return new Result(request.Slug, list.ToList(), request.SortOrder, request.Year, bunch.Currency.Format, bunch.Currency.ThousandSeparator);
        }
        
        private static IEnumerable<ListCashgame> SortItems(IEnumerable<ListCashgame> items, SortOrder orderBy)
        {
            switch (orderBy)
            {
                case SortOrder.PlayerCount:
                    return items.OrderByDescending(o => o.PlayerCount);
                case SortOrder.Duration:
                    return items.OrderByDescending(o => o.Duration);
                case SortOrder.Turnover:
                    return items.OrderByDescending(o => o.Turnover);
                case SortOrder.AverageBuyin:
                    return items.OrderByDescending(o => o.AverageBuyin);
                default:
                    return items.OrderByDescending(o => o.StartTime);
            }
        }

        public class Request
        {
            public string Slug { get; }
            public SortOrder SortOrder { get; }
            public int? Year { get; }

            public Request(string slug, SortOrder sortOrder, int? year)
            {
                Slug = slug;
                SortOrder = sortOrder;
                Year = year;
            }
        }

        public class Result
        {
            public IList<Item> List { get; private set; }
            public SortOrder SortOrder { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }
            public bool ShowYear { get; private set; }
            public string CurrencyFormat { get; private set; }
            public string ThousandSeparator { get; private set; }

            public Result(string slug, IList<Item> list, SortOrder sortOrder, int? year, string currencyFormat, string thousandSeparator)
            {
                Slug = slug;
                List = list;
                SortOrder = sortOrder;
                Year = year;
                ShowYear = year.HasValue;
                CurrencyFormat = currencyFormat;
                ThousandSeparator = thousandSeparator;
            }
        }

        public class Item
        {
            public string Location { get; private set; }
            public string CashgameId { get; private set; }
            public Time Duration { get; private set; }
            public Date Date { get; private set; }
            public Money Turnover { get; private set; }
            public Money AverageBuyin { get; private set; }
            public int PlayerCount { get; private set; }

            public Item(Bunch bunch, ListCashgame cashgame)
            {
                Location = cashgame.Location.Name;
                CashgameId = cashgame.Id;
                Duration = Time.FromMinutes(cashgame.Duration);
                Date = new Date(cashgame.StartTime);
                Turnover = new Money(cashgame.Turnover, bunch.Currency);
                AverageBuyin = new Money(cashgame.AverageBuyin, bunch.Currency);
                PlayerCount = cashgame.PlayerCount;
            }
        }

        public enum SortOrder
        {
            Date,
            PlayerCount,
            Duration,
            Turnover,
            AverageBuyin
        }
    }
}