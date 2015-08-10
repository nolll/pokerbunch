using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

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
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            cashgames = SortItems(cashgames, request.SortOrder).ToList();
            var spansMultipleYears = CashgameService.SpansMultipleYears(cashgames);
            var list = cashgames.Select(o => new Item(bunch, o));

            return new Result(request.Slug, list.ToList(), request.SortOrder, request.Year, spansMultipleYears);
        }

        private static IEnumerable<Cashgame> SortItems(IEnumerable<Cashgame> items, SortOrder orderBy)
        {
            switch (orderBy)
            {
                case SortOrder.PlayerCount:
                    return items.OrderByDescending(o => o.PlayerCount);
                case SortOrder.Location:
                    return items.OrderByDescending(o => o.Location);
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
            public string Slug { get; private set; }
            public SortOrder SortOrder { get; private set; }
            public int? Year { get; private set; }

            public Request(string slug, string orderBy, int? year)
                : this(slug, ParseToplistSortOrder(orderBy), year)
            {
            }

            private Request(string slug, SortOrder sortOrder, int? year)
            {
                Slug = slug;
                SortOrder = sortOrder;
                Year = year;
            }

            private static SortOrder ParseToplistSortOrder(string s)
            {
                if (s == null)
                    return SortOrder.Date;
                SortOrder sortOrder;
                return Enum.TryParse(s, true, out sortOrder) ? sortOrder : SortOrder.Date;
            }
        }

        public class Result
        {
            public IList<Item> List { get; private set; }
            public SortOrder SortOrder { get; private set; }
            public string Slug { get; private set; }
            public int? Year { get; private set; }
            public bool ShowYear { get; private set; }
            public bool SpansMultipleYears { get; private set; }

            public Result(string slug, IList<Item> list, SortOrder sortOrder, int? year, bool spansMultipleYears)
            {
                Slug = slug;
                List = list;
                SortOrder = sortOrder;
                Year = year;
                ShowYear = year.HasValue;
                SpansMultipleYears = spansMultipleYears;
            }
        }

        public class Item
        {
            public string Location { get; private set; }
            public Url Url { get; private set; }
            public Time Duration { get; private set; }
            public Date Date { get; private set; }
            public Money Turnover { get; private set; }
            public Money AverageBuyin { get; private set; }
            public int PlayerCount { get; private set; }

            public Item(Bunch bunch, Cashgame cashgame)
            {
                Location = cashgame.Location;
                Url = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString);
                Duration = Time.FromMinutes(cashgame.Duration);
                Date = cashgame.StartTime.HasValue ? new Date(cashgame.StartTime.Value) : new Date(DateTime.MinValue);
                Turnover = new Money(cashgame.Turnover, bunch.Currency);
                AverageBuyin = new Money(cashgame.AverageBuyin, bunch.Currency);
                PlayerCount = cashgame.PlayerCount;
            }
        }

        public enum SortOrder
        {
            Date,
            PlayerCount,
            Location,
            Duration,
            Turnover,
            AverageBuyin
        }
    }
}