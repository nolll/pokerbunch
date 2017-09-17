using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class CashgameList
    {
        private readonly IBunchService _bunchService;
        private readonly ICashgameService _cashgameService;

        public CashgameList(IBunchService bunchService, ICashgameService cashgameService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);
            var cashgames = _cashgameService.List(bunch.Id, request.Year).Where(o => !o.IsRunning);
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
            public IList<Item> List { get; }
            public SortOrder SortOrder { get; }
            public string Slug { get; }
            public int? Year { get; }
            public bool ShowYear { get; }
            public string CurrencyFormat { get; }
            public string ThousandSeparator { get; }

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
            public string Location { get; }
            public string CashgameId { get; }
            public Time Duration { get; }
            public Date Date { get; }
            public Money Turnover { get; }
            public Money AverageBuyin { get; }
            public int PlayerCount { get; }

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